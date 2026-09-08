using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoRestClientPerpetualFuturesSharedApi
    {

        #region Get Ticker

        async Task<ICallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker>> IGetTickerRest.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var result = await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker>(result);

            return HttpResult.Ok<SharedTicker>(result, result.Data);
        }

        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions => GetTickerOptions;

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Required("SettleAsset", "Settlement asset, btc, usd or usdt", "usdt")
            ]
        };
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var resultContract = _api.ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct);
            var resultTicker = _api.ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultContract, resultTicker).ConfigureAwait(false);

            if (!resultContract.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultContract.Result);
            if (!resultTicker.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result);

            var ticker = resultTicker.Result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol not found")));

            return HttpResult.Ok(resultContract.Result,
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, resultContract.Result.Data.Name),
                    resultContract.Result.Data.Name,
                    ticker.LastPrice,
                    ticker.HighPrice,
                    ticker.LowPrice,
                    new SharedOrderQuantity(ticker.BaseVolume, ticker.QuoteVolume, ticker.Volume),
                    ticker.ChangePercentage)
            {
                MarkPrice = ticker.MarkPrice,
                IndexPrice = ticker.IndexPrice,
                FundingRate = ticker.FundingRate,
                NextFundingTime = resultContract.Result.Data.NextFundingTime
            });
        }

        #endregion

        #region Get All Tickers

        async Task<ICallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker[]>> IGetAllTickersRest.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var result = await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker[]>(result);

            return HttpResult.Ok<SharedTicker[]>(result, result.Data);
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllTickersOptions;

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Required("SettleAsset", "Settlement asset, btc, usd or usdt", "usdt")
            ]
        };
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var resultTickers = _api.ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            var resultContracts = _api.ExchangeData.GetContractsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            await Task.WhenAll(resultTickers, resultContracts).ConfigureAwait(false);
            if (!resultTickers.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultTickers.Result);
            if (!resultContracts.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultContracts.Result);

            return HttpResult.Ok<SharedFuturesTicker[]>(resultTickers.Result, resultTickers.Result.Data.Select(x =>
            {
                var contract = resultContracts.Result.Data.Single(p => p.Name == x.Contract);
                if (request.TradingMode != null)
                {
                    if ((request.TradingMode == TradingMode.PerpetualLinear && contract.Type == ContractType.Inverse)
                    || (request.TradingMode == TradingMode.PerpetualInverse && contract.Type == ContractType.Direct))
                    {
                        return null;
                    }
                }

                return new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Contract),
                    x.Contract,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.BaseVolume, x.QuoteVolume, x.Volume),
                    x.ChangePercentage)
                {
                    IndexPrice = contract.IndexPrice,
                    MarkPrice = contract.MarkPrice,
                    FundingRate = contract.FundingRate,
                    NextFundingTime = contract.NextFundingTime
                };
            }).Where(x => x != null).ToArray()!);
        }

        #endregion

    }
}
