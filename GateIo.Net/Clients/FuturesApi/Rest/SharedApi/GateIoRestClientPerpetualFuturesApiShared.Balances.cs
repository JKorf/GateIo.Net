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
        #region Get Balances

        async Task<IExchangeCallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
            => await GetBalancesAsync(request, ct).ConfigureAwait(false);

        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var resultUsd = _api.Account.GetAccountAsync("usd", ct: ct);
            var resultUsdt = _api.Account.GetAccountAsync("usdt", ct: ct);
            var resultBtc = _api.Account.GetAccountAsync("btc", ct: ct);
            await Task.WhenAll(resultBtc, resultUsdt, resultUsd).ConfigureAwait(false);
            if (!resultUsd.Result.Success && !resultUsd.Result.Error!.ErrorCode!.Contains("NOT_FOUND"))
                return HttpResult.Fail<SharedBalance[]>(resultUsd.Result);
            if (!resultUsdt.Result.Success && !resultUsdt.Result.Error!.ErrorCode!.Contains("USER_NOT_FOUND"))
                return HttpResult.Fail<SharedBalance[]>(resultUsdt.Result);
            if (!resultBtc.Result.Success && !resultBtc.Result.Error!.ErrorCode!.Contains("USER_NOT_FOUND"))
                return HttpResult.Fail<SharedBalance[]>(resultBtc.Result);

            var result = new List<SharedBalance>();
            if (resultUsd.Result.Success)
                result.Add(new SharedBalance(SupportedTradingModes, resultUsd.Result.Data.Asset, resultUsd.Result.Data.Available, resultUsd.Result.Data.Total));
            if (resultUsdt.Result.Success)
                result.Add(new SharedBalance(SupportedTradingModes, resultUsdt.Result.Data.Asset, resultUsdt.Result.Data.Available, resultUsdt.Result.Data.Total));
            if (resultBtc.Result.Success)
                result.Add(new SharedBalance(SupportedTradingModes, resultBtc.Result.Data.Asset, resultBtc.Result.Data.Available, resultBtc.Result.Data.Total));
            return HttpResult.Ok((resultUsd.Result.Success ? resultUsd.Result : resultUsdt.Result.Success ? resultUsdt.Result : resultBtc.Result), result.ToArray());
        }

        #endregion

    }
}
