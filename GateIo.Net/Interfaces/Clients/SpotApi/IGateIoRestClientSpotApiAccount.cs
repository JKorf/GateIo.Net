using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// GateIo Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IGateIoRestClientSpotApiAccount
    {
        /// <summary>
        /// Get spot account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#list-spot-trading-accounts" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/accounts
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoBalance[]>> GetBalancesAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balance changes for the user
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-spot-account-transaction-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/account_book
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="code">["<c>code</c>"] Filter by code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLedgerEntry[]>> GetLedgerAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            string? type = null,
            string? code = null,
            CancellationToken ct = default);

        /// <summary>
        /// Withdraw
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#withdraw" /><br />
        /// Endpoint:<br />
        /// /api/v4/withdrawals
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset to withdraw, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to withdraw</param>
        /// <param name="address">["<c>address</c>"] Withdrawal address</param>
        /// <param name="network">["<c>chain</c>"] Network to use</param>
        /// <param name="memo">["<c>memo</c>"] Memo</param>
        /// <param name="clientOrderId">["<c>withdraw_order_id</c>"] Client specified id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string network, string? memo = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a pending withdrawal
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-withdrawal-with-specified-id" /><br />
        /// Endpoint:<br />
        /// /api/v4/withdrawals/{withdrawalId}
        /// </para>
        /// </summary>
        /// <param name="withdrawalId">Id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawal>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Generate deposit address
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#generate-currency-deposit-address" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/deposit_address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoDepositAddress>> GenerateDepositAddressAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-withdrawal-records" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/withdrawals
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="withdrawalId">["<c>withdraw_id</c>"] Filter by withdrawal id</param>
        /// <param name="assetClass">["<c>asset_class</c>"] Filter by asset class</param>
        /// <param name="withdrawClientOrderId">["<c>withdraw_order_id</c>"] Filter by client order id</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawal[]>> GetWithdrawalsAsync(
            string? asset = null,
            string? withdrawalId = null,
            string? assetClass = null,
            string? withdrawClientOrderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-deposit-records" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/deposits
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoDeposit[]>> GetDepositsAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfer between accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#transfer-between-trading-accounts" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/transfers
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset to transfer, for example `ETH`</param>
        /// <param name="from">["<c>from</c>"] From account</param>
        /// <param name="to">["<c>to</c>"] To account</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to transfer</param>
        /// <param name="marginSymbol">["<c>currency_pair</c>"] Margin symbol, required when from or to account is margin</param>
        /// <param name="settleAsset">["<c>settle</c>"] Settle asset, required when from or to is futures</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTransfer>> TransferAsync(
            string asset,
            AccountType from,
            AccountType to,
            decimal quantity,
            string? marginSymbol = null,
            string? settleAsset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get transfer status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#transfer-status-query" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/order_status
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id, either this or transactionId should be provided</param>
        /// <param name="transactionId">["<c>tx_id</c>"] Transaction id, either this or clientOrderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoTransferStatus>> GetTransferStatusAsync(
            string? clientOrderId = null,
            string? transactionId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get account withdrawal status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-withdrawal-status" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/withdraw_status
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter for a single asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawStatus[]>> GetWithdrawStatusAsync(
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get saved addresses
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-withdrawal-address-whitelist" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/saved_address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>chain</c>"] Filter by network</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSavedAddress[]>> GetSavedAddressAsync(
            string asset,
            string? network = null,
            int? limit = null,
            int? page = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get trading fees
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-personal-trading-fees" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/fee
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Specify a symbol to retrieve precise fee rate, for example `ETH_USDT`</param>
        /// <param name="settleAsset">["<c>settle</c>"] Specify the settlement asset of the contract to get more accurate rate settings</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoFeeRate>> GetTradingFeeAsync(
            string? symbol = null,
            string? settleAsset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get account balance values
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-personal-account-totals" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/total_balance
        /// </para>
        /// </summary>
        /// <param name="valuationAsset">["<c>currency</c>"] Asset unit used to calculate the balance amount</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoAccountValuation>> GetAccountBalancesAsync(
           string? valuationAsset = null,
           CancellationToken ct = default);

        /// <summary>
        /// Get small balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-list-of-convertible-small-balance-currencies" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/small_balance
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSmallBalance[]>> GetSmallBalancesAsync(
            CancellationToken ct = default);

        /// <summary>
        /// Convert small balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#convert-small-balance-currency" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/small_balance
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>currency</c>"] Assets to convert, for example `ETH`</param>
        /// <param name="all">["<c>is_all</c>"] Convert all</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ConvertSmallBalancesAsync(
            IEnumerable<string>? assets = null,
            bool? all = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get small balances conversion history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-convertible-small-balance-currency-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/small_balance_history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSmallBalanceConversion[]>> GetSmallBalanceConversionsAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get unified account info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-unified-account-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/accounts
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountInfo>> GetUnifiedAccountInfoAsync(
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get max borrowable amount
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-maximum-borrowable-amount-for-unified-account" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/borrowable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMax>> GetUnifiedAccountBorrowableAsync(
            string asset,
            CancellationToken ct = default);

        /// <summary>
        /// Get max transferable amount
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-maximum-transferable-amount-for-unified-account" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/transferable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMax>> GetUnifiedAccountTransferableAsync(
            string asset,
            CancellationToken ct = default);

        /// <summary>
        /// Borrow or repay
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#borrow-or-repay" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/loans
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name, for example `ETH`</param>
        /// <param name="direction">["<c>type</c>"] Direction</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="repayAll">["<c>repaid_all</c>"] When set to 'true,' it overrides the 'amount,' allowing for direct full repayment.</param>
        /// <param name="text">["<c>text</c>"] User defined text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> UnifiedAccountBorrowOrRepayAsync(
            string asset,
            BorrowDirection direction,
            decimal quantity,
            bool? repayAll = null,
            string? text = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get loans
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-loans" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/loans
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Limit</param>
        /// <param name="type">["<c>type</c>"] Loan type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLoan[]>> GetUnifiedAccountLoansAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            LoanType? type = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get loan history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-loan-records" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/loan_records
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="direction">["<c>type</c>"] Direction</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLoanRecord[]>> GetUnifiedAccountLoanHistoryAsync(
            string? asset = null,
            BorrowDirection? direction = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get interest history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-interest-deduction-records" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/interest_records
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoInterestRecord[]>> GetUnifiedAccountInterestHistoryAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            LoanType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user risk unit details
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-user-risk-unit-details" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/risk_units
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoRiskUnits>> GetUnifiedAccountRiskUnitsAsync(CancellationToken ct = default);

        /// <summary>
        /// Set unified account mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#set-unified-account-mode" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/unified_mode
        /// </para>
        /// </summary>
        /// <param name="mode">["<c>mode</c>"] New mode</param>
        /// <param name="usdtFutures">["<c>settings.usdt_futures</c>"] USDT contract switch. This parameter is required when the mode is multi-currency margin mode</param>
        /// <param name="spotHedge">["<c>settings.spot_hedge</c>"] Spot hedging switch. This parameter is required when the mode is portfolio margin mode</param>
        /// <param name="useFunding">["<c>settings.use_funding</c>"] When the mode is set to combined margin mode, will funds be used as margin</param>
        /// <param name="options">["<c>settings.options</c>"] Option switch. If not transmitted, the current switch value is used. If not transmitted for the first time, the default value is off</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetUnifiedAccountModeAsync(UnifiedAccountMode mode, bool? usdtFutures = null, bool? spotHedge = null, bool? useFunding = null, bool? options = null, CancellationToken ct = default);

        /// <summary>
        /// Get unified account mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-mode-of-the-unified-account" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/unified_mode
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMode>> GetUnifiedAccountModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get estimated lending rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-unified-account-estimated-interest-rate" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/estimate_rate
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>currencies</c>"] Up to 10 assets, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal?>>> GetUnifiedAccountEstimatedLendingRatesAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// Get unified account min and max leverage rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#maximum-and-minimum-currency-leverage-that-can-be-set" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/leverage/user_currency_config
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoLeverageConfig>> GetUnifiedLeverageConfigsAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the current leverage setttings
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-user-currency-leverage" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/leverage/user_currency_setting
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoLeverageSetting[]>> GetUnifiedLeverageAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Set the leverage for an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#set-loan-currency-leverage" /><br />
        /// Endpoint:<br />
        /// /api/v4/unified/leverage/user_currency_setting
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetUnifiedLeverageAsync(string asset, decimal leverage, CancellationToken ct = default);

        /// <summary>
        /// Get account and API key info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-user-account-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/account/detail
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin account list
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#margin-account-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/accounts
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginAccount[]>> GetMarginAccountsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account list
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-user-s-isolated-margin-account-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/user/account
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoIsolatedMarginAccount[]>> GetIsolatedMarginAccountsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get margin accounts balance change history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-margin-account-balance-change-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/account_book
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginBalanceChange[]>> GetMarginBalanceHistoryAsync(
            string? asset = null,
            string? symbol = null,
            string? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get margin funding accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#funding-account-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/funding_accounts
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginFundingAccount[]>> GetMarginFundingAccountsAsync(
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Set auto repayment
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#update-user-auto-repayment-settings" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/auto_repay
        /// </para>
        /// </summary>
        /// <param name="enabled">["<c>status</c>"] True for auto repayment on, false for auto repayment off</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginAutoRepayStatus>> SetMarginAutoRepayAsync(
            bool enabled,
            CancellationToken ct = default);

        /// <summary>
        /// Get auto repayment setting
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-user-auto-repayment-settings" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/auto_repay
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginAutoRepayStatus>> GetMarginAutoRepayAsync(CancellationToken ct = default);

        /// <summary>
        /// Get max transferable quantity
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-maximum-transferable-amount-for-isolated-margin" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/transferable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginMaxTransferable>> GetMarginMaxTransferableAsync(string asset, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-cross-margin-account" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/accounts
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginAccount>> GetCrossMarginAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-cross-margin-account-change-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/account_book
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBalanceChange[]>> GetCrossMarginBalanceHistoryAsync(string? asset = null,
            string? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-cross-margin-account-change-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/loans
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="text">["<c>text</c>"] User defined text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan>> CreateCrossMarginLoanAsync(
            string asset,
            decimal quantity,
            string? text = null,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#list-cross-margin-borrow-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/loans
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="reverse">["<c>reverse</c>"] Reverse results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan[]>> GetCrossMarginLoansAsync(
            string? asset = null,
            int? limit = null,
            int? offset = null,
            bool? reverse = null,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-single-borrow-loan-detail" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/loans/{id}
        /// </para>
        /// </summary>
        /// <param name="id">Loan id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan>> GetCrossMarginLoanAsync(string id,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-single-borrow-loan-detail" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/repayments
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan[]>> CrossMarginRepayAsync(
            string asset,
            decimal quantity,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-cross-margin-repayments" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/repayments
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="loanId">["<c>loan_id</c>"] Loan id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="reverse">["<c>reverse</c>"] Reverse results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginRepayment[]>> GetCrossMarginRepaymentsAsync(
            string? asset = null,
            string? loanId = null,
            int? limit = null,
            int? offset = null,
            bool? reverse = null,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#interest-records-for-the-cross-margin-account" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/interest_records
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginInterest[]>> GetCrossMarginInterestHistoryAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-the-max-transferable-amount-for-a-specific-cross-margin-currency" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/transferable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginMaxTransferable>> GetCrossMarginMaxTransferableAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#estimated-interest-rates" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/estimate_rate
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>currencies</c>"] Assets, max 10, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetCrossMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-the-max-borrowable-amount-for-a-specific-cross-margin-currency" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/cross/borrowable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMax>> GetCrossMarginMaxBorrowableAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get margin estimated interest rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#estimate-interest-rate-for-isolated-margin-currencies" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/uni/estimate_rate
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>currencies</c>"] Assets, max 10, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// Borrow or repay margin loan
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#borrow-or-repay-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/uni/loans
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol</param>
        /// <param name="direction">["<c>type</c>"] Borrow or repay</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="repayAll">["<c>repaid_all</c>"] Repay all instead of specifying quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> BorrowOrRepayAsync(
            string asset,
            string symbol,
            BorrowDirection direction,
            decimal quantity,
            bool? repayAll = null,
            CancellationToken ct = default);

        /// <summary>
        /// List margin loans
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-loans-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/uni/loans
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLoan[]>> GetMarginLoansAsync(
            string? asset = null,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// List margin loan history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-loan-records-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/uni/loan_records
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="direction">["<c>type</c>"] Filter by direction</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginLoanRecord[]>> GetMarginLoanHistoryAsync(
            string? asset = null,
            string? symbol = null,
            BorrowDirection? direction = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// List margin interest records
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-interest-deduction-records-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/uni/interest_records
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoInterestRecord[]>> GetMarginInterestHistoryAsync(
            string? asset = null,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get margin max borrowable quantity
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-maximum-borrowable-amount-by-currency" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/uni/borrowable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginMaxBorrowable>> GetMarginMaxBorrowableAsync(
            string asset,
            string symbol,
            CancellationToken ct = default);

        /// <summary>
        /// Get GT deduction enabled status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-gt-fee-deduction-configuration" /><br />
        /// Endpoint:<br />
        /// /api/v4/account/debit_fee
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoGTDeducationStatus>> GetGTDeductionStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Set GT deduction enabled status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#configure-gt-fee-deduction" /><br />
        /// Endpoint:<br />
        /// /api/v4/account/debit_fee
        /// </para>
        /// </summary>
        /// <param name="enabled">["<c>enabled</c>"] Enabled</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetGTDeductionStatusAsync(bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Get transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-uid-transfer-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/wallet/push
        /// </para>
        /// </summary>
        /// <param name="id">["<c>id</c>"] Filter by id</param>
        /// <param name="transactionType">["<c>transaction_type</c>"] Filter by transaction type</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoTransferEntry[]>> GetTransferHistoryAsync(long? id = null, TransactionType? transactionType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer to another GateIo account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#uid-transfer" /><br />
        /// Endpoint:<br />
        /// /api/v4/withdrawals/push
        /// </para>
        /// </summary>
        /// <param name="receiveAccountId">["<c>receive_uid</c>"] Account id to transfer to</param>
        /// <param name="asset">["<c>currency</c>"] Asset to transfer</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoId>> TransferToAccountAsync(long receiveAccountId, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get rate limit ratios for the user
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-user-transaction-rate-limit-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/account/rate_limit
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoUserRateLimit[]>> GetRateLimitsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get insurance fund history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-spot-insurance-fund-historical-data" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/insurance_history
        /// </para>
        /// </summary>
        /// <param name="businessType">["<c>business</c>"] Business type</param>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="pageSize">["<c>limit</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoInsuranceFund[]>> GetInsuranceFundHistoryAsync(
            BusinessType businessType,
            string asset,
            DateTime startTime,
            DateTime endTime,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// Set margin leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#set-user-market-leverage-multiplier" /><br />
        /// Endpoint:<br />
        /// /api/v4/margin/leverage/user_market_setting
        /// </para>
        /// </summary>
        /// <param name="leverage">["<c>leverage</c>"] Leverage to set</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetMarginLeverageAsync(decimal leverage, string? symbol = null, CancellationToken ct = default);
    }
}

