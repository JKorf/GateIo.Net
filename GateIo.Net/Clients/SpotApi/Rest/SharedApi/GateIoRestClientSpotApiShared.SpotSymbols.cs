using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Objects.Models;
using CryptoExchange.Net;

namespace GateIo.Net.Clients.SpotApi
{
    internal partial class GateIoRestClientSpotSharedApi
    {
        #region Get Spot Symbols

        async Task<IExchangeCallResult<SharedSpotSymbol[]>> IGetSpotSymbols.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
            => await GetSpotSymbolsAsync(request, ct).ConfigureAwait(false);

        public SharedSymbolCatalog? SpotSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);
        public GetSpotSymbolsOptions GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);

        public async Task<HttpResult<SharedSpotSymbol[]>> GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            var result = await _api.ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);
            if (!assets.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(assets);

            var data = result.Data
               .Select(x => ParseSymbol(x, assets.Data))
               .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(data, request));
        }

        #endregion

        private SharedSpotSymbol ParseSymbol(GateIoSymbol s, GateIoAsset[] assets)
        {
            var baseAsset = assets.FirstOrDefault(a => a.Asset == s.BaseAsset);
            var quoteAsset = assets.FirstOrDefault(a => a.Asset == s.QuoteAsset);

            var result = new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.TradeStatus == SymbolStatus.Tradable)
            {
                MinTradeQuantity = s.MinBaseQuantity,
                MaxTradeQuantity = s.MaxBaseQuantity,
                MinNotionalValue = s.MinQuoteQuantity,
                PriceDecimals = s.PricePrecision,
                QuantityDecimals = s.QuantityPrecision,
                QuoteAssetType = SharedAssetType.Crypto,
                DisplayName = s.Name,
                UpperPriceLimitPercentage = s.PriceUpRateLimit * 100,
                LowerPriceLimitPercentage = -s.PriceDownRateLimit * 100
            };

            if (LibraryHelpers.IsStableCoin(result.QuoteAsset))
                result.QuoteAssetSubType = SharedAssetSubType.StableCoin;

            if (baseAsset == null)
                return result;

            if (baseAsset.Asset.EndsWith("3L")
                || baseAsset.Asset.EndsWith("5L")
                || baseAsset.Asset.EndsWith("3S")
                || baseAsset.Asset.EndsWith("5S"))
            {
                var withoutSuffix = baseAsset.Asset.Substring(0, baseAsset.Asset.Length - 2);
                baseAsset = assets.FirstOrDefault(x => x.Asset == withoutSuffix) ?? baseAsset;
            }

            if (baseAsset.Categories.Contains(AssetCategory.Stocks)
                || baseAsset.Categories.Contains(AssetCategory.GStocks)
                || baseAsset.Categories.Contains(AssetCategory.OndoStocks)
                || baseAsset.Categories.Contains(AssetCategory.XStocks)
                || baseAsset.Categories.Contains(AssetCategory.Indices))
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Equity;
            }
            else if (baseAsset.Categories.Contains(AssetCategory.Metals)
                || baseAsset.Categories.Contains(AssetCategory.Commodities))
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Commodity;
            }
            else if (baseAsset.Categories.Contains(AssetCategory.Forex))
            {
                if (LibraryHelpers.IsStableCoin(result.BaseAsset))
                {
                    result.BaseAssetType = SharedAssetType.Crypto;
                    result.BaseAssetSubType = SharedAssetSubType.StableCoin;
                }
                else
                {
                    result.BaseAssetType = SharedAssetType.Fiat;
                }
            }
            else
            {

                result.BaseAssetType = SharedAssetType.Crypto;
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
    }
}
