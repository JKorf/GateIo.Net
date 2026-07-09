using CryptoExchange.Net.Objects;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Linq;

namespace GateIo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class GateIoRestClientSpotApiAccount : IGateIoRestClientSpotApiAccount
    {
        private readonly GateIoRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal GateIoRestClientSpotApiAccount(GateIoRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<GateIoBalance[]>> GetBalancesAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/accounts", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoBalance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLedgerEntry[]>> GetLedgerAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            string? type = null,
            string? code = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("type", type);
            parameters.Add("code", code);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/account_book", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<GateIoWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string network, string? memo = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("address", address);
            parameters.Add("chain", network);
            parameters.Add("withdraw_order_id", clientOrderId);
            parameters.Add("memo", memo);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/withdrawals", GateIoExchange.RateLimiter.RestSpotOther, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoWithdrawal>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<HttpResult<GateIoWithdrawal>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v4/withdrawals/" + withdrawalId, GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoWithdrawal>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Generate Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<GateIoDepositAddress>> GenerateDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/deposit_address", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoDepositAddress>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawals

        /// <inheritdoc />
        public async Task<HttpResult<GateIoWithdrawal[]>> GetWithdrawalsAsync(
            string? asset = null,
            string? withdrawalId = null,
            string? assetClass = null,
            string? withdrawClientOrderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("withdraw_id", withdrawalId);
            parameters.Add("asset_class", assetClass);
            parameters.Add("withdraw_order_id", withdrawClientOrderId);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/withdrawals", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<HttpResult<GateIoDeposit[]>> GetDepositsAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/deposits", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoDeposit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTransfer>> TransferAsync(
            string asset,
            AccountType from,
            AccountType to,
            decimal quantity,
            string? marginSymbol = null,
            string? settleAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("amount", quantity);
            parameters.Add("currency_pair", marginSymbol);
            parameters.Add("settle", settleAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/wallet/transfers", GateIoExchange.RateLimiter.RestSpotOther, 1, true,
                limitGuard: new SingleLimitGuard(80, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer Status

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTransferStatus>> GetTransferStatusAsync(
            string? clientOrderId = null,
            string? transactionId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("tx_id", transactionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/wallet/order_status", GateIoExchange.RateLimiter.RestSpotOther, 1, true,
                limitGuard: new SingleLimitGuard(80, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoTransferStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer To Account

        /// <inheritdoc />
        public async Task<HttpResult<GateIoId>> TransferToAccountAsync(long receiveAccountId, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("receive_uid", receiveAccountId);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/withdrawals/push", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            var result = await _baseClient.SendAsync<GateIoId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


        #region Get Withdraw Status

        /// <inheritdoc />
        public async Task<HttpResult<GateIoWithdrawStatus[]>> GetWithdrawStatusAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/withdraw_status", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoWithdrawStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Saved Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<GateIoSavedAddress[]>> GetSavedAddressAsync(
            string? asset = null,
            string? network = null,
            bool? verified = null,
            int? limit = null,
            int? page = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            parameters.Add("verified", verified == null ? null : verified.Value ? 1 : 0);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/saved_address", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSavedAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fee

        /// <inheritdoc />
        public async Task<HttpResult<GateIoFeeRate>> GetTradingFeeAsync(
            string? symbol = null,
            string? settleAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("settle", settleAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/fee", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoFeeRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Balances

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAccountValuation>> GetAccountBalancesAsync(
            string? valuationAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", valuationAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/total_balance", GateIoExchange.RateLimiter.Public, 1, true,
                limitGuard: new SingleLimitGuard(80, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoAccountValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Balances

        /// <inheritdoc />
        public async Task<HttpResult<GateIoSmallBalance[]>> GetSmallBalancesAsync(
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/small_balance", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSmallBalance[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert Small Balances

        /// <inheritdoc />
        public async Task<HttpResult> ConvertSmallBalancesAsync(
            IEnumerable<string>? assets = null,
            bool? all = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.AddArray("currency", assets?.ToArray());
            parameters.Add("is_all", all);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/wallet/small_balance", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Balances Conversions

        /// <inheritdoc />
        public async Task<HttpResult<GateIoSmallBalanceConversion[]>> GetSmallBalanceConversionsAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/small_balance_history", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSmallBalanceConversion[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTransferEntry[]>> GetTransferHistoryAsync(long? id = null, TransactionType? transactionType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("id", id);
            parameters.Add("from", startTime, DateTimeSerialization.MillisecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.MillisecondsNumber);
            parameters.Add("limit", limit);
            parameters.Add("transaction_type", transactionType);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/wallet/push", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            var result = await _baseClient.SendAsync<GateIoTransferEntry[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Unified Account Info

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUnifiedAccountInfo>> GetUnifiedAccountInfoAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Borrowable

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUnifiedAccountMax>> GetUnifiedAccountBorrowableAsync(
            string asset,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/borrowable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMax>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Transferable

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUnifiedAccountMax>> GetUnifiedAccountTransferableAsync(
            string asset,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/transferable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMax>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Unified Account Borrow Or Repay

        /// <inheritdoc />
        public async Task<HttpResult> UnifiedAccountBorrowOrRepayAsync(
            string asset,
            BorrowDirection direction,
            decimal quantity,
            bool? repayAll = null,
            string? text = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("type", direction);
            parameters.Add("amount", quantity);
            parameters.Add("repaid_all", repayAll);
            parameters.Add("text", text);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/unified/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true,
                limitGuard: new SingleLimitGuard(15, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Loans

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLoan[]>> GetUnifiedAccountLoansAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            LoanType? type = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Loan History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLoanRecord[]>> GetUnifiedAccountLoanHistoryAsync(
            string? asset = null,
            BorrowDirection? direction = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("type", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/loan_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoLoanRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Interest History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoInterestRecord[]>> GetUnifiedAccountInterestHistoryAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            LoanType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("type", type);
            parameters.Add("from", startTime, DateTimeSerialization.MillisecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.MillisecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/interest_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoInterestRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Risk Units

        /// <inheritdoc />
        public async Task<HttpResult<GateIoRiskUnits>> GetUnifiedAccountRiskUnitsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/risk_units", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoRiskUnits>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Unified Account Mode

        /// <inheritdoc />
        public async Task<HttpResult> SetUnifiedAccountModeAsync(UnifiedAccountMode mode, bool? usdtFutures = null, bool? spotHedge = null, bool? useFunding = null, bool? options = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("mode", mode);
            if (usdtFutures != null || spotHedge != null || useFunding != null)
            {
                var inner = new Parameters(GateIoExchange._parameterSerializationSettings);
                inner.Add("usdt_futures", usdtFutures);
                inner.Add("spot_hedge", spotHedge);
                inner.Add("use_funding", useFunding);
                inner.Add("options", options);
                parameters.Add("settings", inner);
            }
            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "/api/v4/unified/unified_mode", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Mode

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUnifiedAccountMode>> GetUnifiedAccountModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/unified_mode", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMode>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Estimated Lending Rates

        /// <inheritdoc />
        public async Task<HttpResult<Dictionary<string, decimal?>>> GetUnifiedAccountEstimatedLendingRatesAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currencies", string.Join(",", assets));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/estimate_rate", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, decimal?>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Leverage Configs

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLeverageConfig>> GetUnifiedLeverageConfigsAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/leverage/user_currency_config", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<GateIoLeverageConfig>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Unified Leverage

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLeverageSetting[]>> GetUnifiedLeverageAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/unified/leverage/user_currency_setting", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            if (asset == null)
            {
                return await _baseClient.SendAsync<GateIoLeverageSetting[]>(request, parameters, ct).ConfigureAwait(false);
            }
            else
            {
                var result = await _baseClient.SendAsync<GateIoLeverageSetting>(request, parameters, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<GateIoLeverageSetting[]>(result);

                return HttpResult.Ok<GateIoLeverageSetting[]>(result, [result.Data]);
            }
        }

        #endregion

        #region Set Unified Leverage

        /// <inheritdoc />
        public async Task<HttpResult> SetUnifiedLeverageAsync(string asset, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/unified/leverage/user_currency_setting", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/account/detail", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoAccountInfo>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Accounts

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginAccount[]>> GetMarginAccountsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Accounts

        /// <inheritdoc />
        public async Task<HttpResult<GateIoIsolatedMarginAccount[]>> GetIsolatedMarginAccountsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/user/account", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoIsolatedMarginAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Balance History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginBalanceChange[]>> GetMarginBalanceHistoryAsync(
            string? asset = null,
            string? symbol = null,
            string? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            parameters.Add("type", type);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/account_book", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginBalanceChange[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Funding Accounts

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginFundingAccount[]>> GetMarginFundingAccountsAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/funding_accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginFundingAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Auto Repay

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginAutoRepayStatus>> SetMarginAutoRepayAsync(
            bool enabled,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("status", enabled ? "on" : "off");
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/margin/auto_repay", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginAutoRepayStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Auto Repay

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginAutoRepayStatus>> GetMarginAutoRepayAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/auto_repay", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginAutoRepayStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Max Transferable

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginMaxTransferable>> GetMarginMaxTransferableAsync(string asset, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/transferable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginMaxTransferable>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Accounts

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginAccount>> GetCrossMarginAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginAccount>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Balance History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginBalanceChange[]>> GetCrossMarginBalanceHistoryAsync(string? asset = null,
            string? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("type", type);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/account_book", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBalanceChange[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create Cross Margin Borrow Loan

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginBorrowLoan>> CreateCrossMarginLoanAsync(
            string asset,
            decimal quantity,
            string? text = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("text", text);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/margin/cross/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Loans

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginBorrowLoan[]>> GetCrossMarginLoansAsync(
            string? asset = null,
            int? limit = null,
            int? offset = null,
            bool? reverse = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("limit", limit);
            parameters.Add("reverse", reverse);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Loan

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginBorrowLoan>> GetCrossMarginLoanAsync(string id,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/loans/" + id, GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cross Margin Repay

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginBorrowLoan[]>> CrossMarginRepayAsync(
            string asset,
            decimal quantity,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/margin/cross/repayments", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Repayments

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginRepayment[]>> GetCrossMarginRepaymentsAsync(
            string? asset = null,
            string? loanId = null,
            int? limit = null,
            int? offset = null,
            bool? reverse = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("limit", limit);
            parameters.Add("loan_id", loanId);
            parameters.Add("reverse", reverse);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/repayments", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginRepayment[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Interest History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginInterest[]>> GetCrossMarginInterestHistoryAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/interest_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginInterest[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Max Transferable

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginMaxTransferable>> GetCrossMarginMaxTransferableAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/transferable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginMaxTransferable>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Estimated Interest Rates

        /// <inheritdoc />
        public async Task<HttpResult<Dictionary<string, decimal>>> GetCrossMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currencies", string.Join(",", assets));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/estimate_rate", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, decimal>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Max Borrowable

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUnifiedAccountMax>> GetCrossMarginMaxBorrowableAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/cross/borrowable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMax>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Estimated Interest Rates

        /// <inheritdoc />
        public async Task<HttpResult<Dictionary<string, decimal>>> GetMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currencies", string.Join(",", assets));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/uni/estimate_rate", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, decimal>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Borrow Or Repay

        /// <inheritdoc />
        public async Task<HttpResult> BorrowOrRepayAsync(
            string asset,
            string symbol,
            BorrowDirection direction,
            decimal quantity,
            bool? repayAll = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("type", direction);
            parameters.Add("amount", quantity);
            parameters.Add("repaid_all", repayAll);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/margin/uni/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Loans

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLoan[]>> GetMarginLoansAsync(
            string? asset = null,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/uni/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Loan History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginLoanRecord[]>> GetMarginLoanHistoryAsync(
            string? asset = null,
            string? symbol = null,
            BorrowDirection? direction = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("type", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/uni/loan_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginLoanRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Interest History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoInterestRecord[]>> GetMarginInterestHistoryAsync(
            string? asset = null,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/uni/interest_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoInterestRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Max Borrowable

        /// <inheritdoc />
        public async Task<HttpResult<GateIoMarginMaxBorrowable>> GetMarginMaxBorrowableAsync(
            string asset,
            string symbol,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/margin/uni/borrowable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginMaxBorrowable>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get GT Deduction Status

        /// <inheritdoc />
        public async Task<HttpResult<GateIoGTDeducationStatus>> GetGTDeductionStatusAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/account/debit_fee", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoGTDeducationStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set GT Deduction Status

        /// <inheritdoc />
        public async Task<HttpResult> SetGTDeductionStatusAsync(bool enabled, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("enabled", enabled);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/account/debit_fee", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rate Limits

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUserRateLimit[]>> GetRateLimitsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/account/rate_limit", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            var result = await _baseClient.SendAsync<GateIoUserRateLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Insurance Fund History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoInsuranceFund[]>> GetInsuranceFundHistoryAsync(
            BusinessType businessType,
            string asset,
            DateTime startTime,
            DateTime endTime,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("business", businessType);
            parameters.Add("currency", asset);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/insurance_history", GateIoExchange.RateLimiter.Public, 1, true);
            return await _baseClient.SendAsync<GateIoInsuranceFund[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Leverage
        /// <inheritdoc />
        public async Task<HttpResult> SetMarginLeverageAsync(decimal leverage, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/margin/leverage/user_market_setting", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }
        #endregion
    }
}
