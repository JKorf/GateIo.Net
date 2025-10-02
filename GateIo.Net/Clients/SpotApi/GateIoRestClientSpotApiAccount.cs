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
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
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
        public async Task<WebCallResult<GateIoBalance[]>> GetBalancesAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/accounts", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoBalance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLedgerEntry[]>> GetLedgerAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            string? type = null,
            string? code = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("type", type);
            parameters.AddOptional("code", type);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/account_book", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string network, string? memo = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("address", address);
            parameters.Add("chain", network);
            parameters.AddOptional("withdraw_order_id", clientOrderId);
            parameters.AddOptional("memo", memo);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/withdrawals", GateIoExchange.RateLimiter.RestSpotOther, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoWithdrawal>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoWithdrawal>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v4/withdrawals/" + withdrawalId, GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoWithdrawal>(request, null, ct).ConfigureAwait(false);
        }

        #endregion    
        
        #region Generate Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoDepositAddress>> GenerateDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/deposit_address", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoDepositAddress>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawals

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoWithdrawal[]>> GetWithdrawalsAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("withdraw_id", withdrawalId);
            parameters.AddOptional("asset_class", assetClass);
            parameters.AddOptional("withdraw_order_id", withdrawClientOrderId);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/withdrawals", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoDeposit[]>> GetDepositsAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/deposits", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoDeposit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTransfer>> TransferAsync(
            string asset,
            AccountType from,
            AccountType to,
            decimal quantity,
            string? marginSymbol = null,
            string? settleAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddEnum("from", from);
            parameters.AddEnum("to", to);
            parameters.AddString("amount", quantity);
            parameters.AddOptional("currency_pair", marginSymbol);
            parameters.AddOptional("settle", settleAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/wallet/transfers", GateIoExchange.RateLimiter.RestSpotOther, 1, true,
                limitGuard: new SingleLimitGuard(80, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer Status

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTransferStatus>> GetTransferStatusAsync(
            string? clientOrderId = null,
            string? transactionId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptional("tx_id", transactionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/wallet/order_status", GateIoExchange.RateLimiter.RestSpotOther, 1, true,
                limitGuard: new SingleLimitGuard(80, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoTransferStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer To Account

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoId>> TransferToAccountAsync(long receiveAccountId, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("receive_uid", receiveAccountId);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/withdrawals/push", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            var result = await _baseClient.SendAsync<GateIoId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


        #region Get Withdraw Status

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoWithdrawStatus[]>> GetWithdrawStatusAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/withdraw_status", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoWithdrawStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Saved Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoSavedAddress[]>> GetSavedAddressAsync(
            string asset,
            string? network = null,
            int? limit = null,
            int? page = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddOptional("chain", network);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/saved_address", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSavedAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fee

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoFeeRate>> GetTradingFeeAsync(
            string? symbol = null,
            string? settleAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("settle", settleAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/fee", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoFeeRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Balances

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAccountValuation>> GetAccountBalancesAsync(
            string? valuationAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", valuationAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/total_balance", GateIoExchange.RateLimiter.Public, 1, true,
                limitGuard: new SingleLimitGuard(80, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<GateIoAccountValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Balances

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoSmallBalance[]>> GetSmallBalancesAsync(
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/small_balance", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSmallBalance[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert Small Balances

        /// <inheritdoc />
        public async Task<WebCallResult> ConvertSmallBalancesAsync(
            IEnumerable<string>? assets = null,
            bool? all = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", assets?.ToArray());
            parameters.AddOptional("is_all", all);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/wallet/small_balance", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Balances Conversions

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoSmallBalanceConversion[]>> GetSmallBalanceConversionsAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/small_balance_history", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSmallBalanceConversion[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTransferEntry[]>> GetTransferHistoryAsync(long? id = null, TransactionType? transactionType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("id", id);
            parameters.AddOptionalMilliseconds("from", startTime);
            parameters.AddOptionalMilliseconds("to", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("transaction_type", transactionType);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/wallet/push", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            var result = await _baseClient.SendAsync<GateIoTransferEntry[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


        #region Get Unified Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUnifiedAccountInfo>> GetUnifiedAccountInfoAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Borrowable

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUnifiedAccountMax>> GetUnifiedAccountBorrowableAsync(
            string asset,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/borrowable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMax>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Transferable

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUnifiedAccountMax>> GetUnifiedAccountTransferableAsync(
            string asset,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/transferable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMax>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Unified Account Borrow Or Repay

        /// <inheritdoc />
        public async Task<WebCallResult> UnifiedAccountBorrowOrRepayAsync(
            string asset,
            BorrowDirection direction,
            decimal quantity,
            bool? repayAll = null,
            string? text = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddEnum("type", direction);
            parameters.AddString("amount", quantity);
            parameters.AddOptional("repaid_all", repayAll);
            parameters.AddOptional("text", text);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/unified/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true,
                limitGuard: new SingleLimitGuard(15, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Loans

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLoan[]>> GetUnifiedAccountLoansAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            LoanType? type = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Loan History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLoanRecord[]>> GetUnifiedAccountLoanHistoryAsync(
            string? asset = null,
            BorrowDirection? direction = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("type", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/loan_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoLoanRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoInterestRecord[]>> GetUnifiedAccountInterestHistoryAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            LoanType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalMilliseconds("from", startTime);
            parameters.AddOptionalMilliseconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/interest_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoInterestRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Risk Units

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoRiskUnits>> GetUnifiedAccountRiskUnitsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/risk_units", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoRiskUnits>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Unified Account Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SetUnifiedAccountModeAsync(UnifiedAccountMode mode, bool? usdtFutures = null, bool? spotHedge = null, bool? useFunding = null, bool? options = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("mode", mode);
            if (usdtFutures != null || spotHedge != null || useFunding != null)
            {
                var inner = new ParameterCollection();
                inner.AddOptional("usdt_futures", usdtFutures);
                inner.AddOptional("spot_hedge", spotHedge);
                inner.AddOptional("use_funding", useFunding);
                inner.AddOptional("options", options);
                parameters.Add("settings", inner);
            }
            var request = _definitions.GetOrCreate(HttpMethod.Put, "/api/v4/unified/unified_mode", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Mode

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUnifiedAccountMode>> GetUnifiedAccountModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/unified_mode", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMode>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Unified Account Estimated Lending Rates

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal?>>> GetUnifiedAccountEstimatedLendingRatesAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currencies", string.Join(",", assets));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/estimate_rate", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, decimal?>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
        
        #region Get Unified Leverage Configs

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLeverageConfig>> GetUnifiedLeverageConfigsAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/leverage/user_currency_config", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<GateIoLeverageConfig>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Unified Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLeverageSetting[]>> GetUnifiedLeverageAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/unified/leverage/user_currency_setting", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            if (asset == null)
            {
                return await _baseClient.SendAsync<GateIoLeverageSetting[]>(request, parameters, ct).ConfigureAwait(false);
            }
            else
            {
                var result = await _baseClient.SendAsync<GateIoLeverageSetting>(request, parameters, ct).ConfigureAwait(false);
                return result.As<GateIoLeverageSetting[]>([result.Data]);
            }
        }

        #endregion

        #region Set Unified Leverage

        /// <inheritdoc />
        public async Task<WebCallResult> SetUnifiedLeverageAsync(string asset, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/unified/leverage/user_currency_setting", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/account/detail", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoAccountInfo>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginAccount[]>> GetMarginAccountsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoIsolatedMarginAccount[]>> GetIsolatedMarginAccountsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/user/account", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoIsolatedMarginAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Balance History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginBalanceChange[]>> GetMarginBalanceHistoryAsync(
            string? asset = null,
            string? symbol = null,
            string? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("type", type);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/account_book", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginBalanceChange[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Funding Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginFundingAccount[]>> GetMarginFundingAccountsAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/funding_accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginFundingAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Auto Repay

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginAutoRepayStatus>> SetMarginAutoRepayAsync(
            bool enabled,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("status", enabled ? "on" : "off");
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/margin/auto_repay", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginAutoRepayStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Auto Repay

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginAutoRepayStatus>> GetMarginAutoRepayAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/auto_repay", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginAutoRepayStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Max Transferable

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginMaxTransferable>> GetMarginMaxTransferableAsync(string asset, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddOptional("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/transferable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginMaxTransferable>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginAccount>> GetCrossMarginAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/accounts", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginAccount>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Balance History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginBalanceChange[]>> GetCrossMarginBalanceHistoryAsync(string? asset = null,
            string? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("type", type);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/account_book", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBalanceChange[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create Cross Margin Borrow Loan

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginBorrowLoan>> CreateCrossMarginLoanAsync(
            string asset,
            decimal quantity,
            string? text = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.AddOptional("text", text);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/margin/cross/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Loans

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginBorrowLoan[]>> GetCrossMarginLoansAsync(
            string? asset = null,
            int? limit = null,
            int? offset = null,
            bool? reverse = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("reverse", reverse);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Loan

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginBorrowLoan>> GetCrossMarginLoanAsync(string id,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/loans/" + id, GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cross Margin Repay

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginBorrowLoan[]>> CrossMarginRepayAsync(
            string asset,
            decimal quantity,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/margin/cross/repayments", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginBorrowLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Repayments

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginRepayment[]>> GetCrossMarginRepaymentsAsync(
            string? asset = null,
            string? loanId = null,
            int? limit = null,
            int? offset = null,
            bool? reverse = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("loan_id", loanId);
            parameters.AddOptional("reverse", reverse);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/repayments", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginRepayment[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginInterest[]>> GetCrossMarginInterestHistoryAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/interest_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoCrossMarginInterest[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Max Transferable

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginMaxTransferable>> GetCrossMarginMaxTransferableAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/transferable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginMaxTransferable>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Estimated Interest Rates

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetCrossMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currencies", string.Join(",", assets));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/estimate_rate", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, decimal>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Max Borrowable

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUnifiedAccountMax>> GetCrossMarginMaxBorrowableAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/cross/borrowable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoUnifiedAccountMax>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Estimated Interest Rates

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currencies", string.Join(",", assets));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/uni/estimate_rate", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, decimal>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Borrow Or Repay

        /// <inheritdoc />
        public async Task<WebCallResult> BorrowOrRepayAsync(
            string asset,
            string symbol,
            BorrowDirection direction,
            decimal quantity,
            bool? repayAll = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddEnum("type", direction);
            parameters.AddString("amount", quantity);
            parameters.AddOptional("repaid_all", repayAll);
            parameters.AddOptional("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/margin/uni/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Loans

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLoan[]>> GetMarginLoansAsync(
            string? asset = null,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/uni/loans", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoLoan[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Loan History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginLoanRecord[]>> GetMarginLoanHistoryAsync(
            string? asset = null,
            string? symbol = null,
            BorrowDirection? direction = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("type", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/uni/loan_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginLoanRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoInterestRecord[]>> GetMarginInterestHistoryAsync(
            string? asset = null,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/uni/interest_records", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoInterestRecord[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Max Borrowable

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoMarginMaxBorrowable>> GetMarginMaxBorrowableAsync(
            string asset,
            string symbol,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/margin/uni/borrowable", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoMarginMaxBorrowable>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get GT Deduction Status

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoGTDeducationStatus>> GetGTDeductionStatusAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/account/debit_fee", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<GateIoGTDeducationStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set GT Deduction Status

        /// <inheritdoc />
        public async Task<WebCallResult> SetGTDeductionStatusAsync(bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("enabled", enabled);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/account/debit_fee", GateIoExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rate Limits

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUserRateLimit[]>> GetRateLimitsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/account/rate_limit", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            var result = await _baseClient.SendAsync<GateIoUserRateLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Insurance Fund History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoInsuranceFund[]>> GetInsuranceFundHistoryAsync(
            BusinessType businessType,
            string asset,
            DateTime startTime,
            DateTime endTime,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("business", businessType);
            parameters.Add("currency", asset);
            parameters.AddSeconds("from", startTime);
            parameters.AddSeconds("to", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/insurance_history", GateIoExchange.RateLimiter.Public, 1, true);
            return await _baseClient.SendAsync<GateIoInsuranceFund[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
