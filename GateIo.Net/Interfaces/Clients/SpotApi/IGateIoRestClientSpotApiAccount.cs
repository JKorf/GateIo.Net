using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// GateIo Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IGateIoRestClientSpotApiAccount
    {
        /// <summary>
        /// Get spot account balances
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-spot-accounts" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoBalance[]>> GetBalancesAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balance changes for the user
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-account-book" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="type">Filter by type</param>
        /// <param name="code">Filter by code</param>
        /// <param name="ct">Cancelation token</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#withdraw" /></para>
        /// </summary>
        /// <param name="asset">Asset to withdraw, for example `ETH`</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Withdrawal address</param>
        /// <param name="network">Network to use</param>
        /// <param name="memo">Memo</param>
        /// <param name="clientOrderId">Client specified id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string network, string? memo = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a pending withdrawal
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#cancel-withdrawal-with-specified-id" /></para>
        /// </summary>
        /// <param name="withdrawalId">Id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawal>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Generate deposit address
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#generate-currency-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">Asset name, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoDepositAddress>> GenerateDepositAddressAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#retrieve-withdrawal-records" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="withdrawalId">Filter by withdrawal id</param>
        /// <param name="assetClass">Filter by asset class</param>
        /// <param name="withdrawClientOrderId">Filter by client order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#retrieve-deposit-records" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#transfer-between-trading-accounts" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer, for example `ETH`</param>
        /// <param name="from">From account</param>
        /// <param name="to">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="marginSymbol">Margin symbol, required when from or to account is margin</param>
        /// <param name="settleAsset">Settle asset, required when from or to is futures</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#transfer-status-query" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id, either this or transactionId should be provided</param>
        /// <param name="transactionId">Transaction id, either this or clientOrderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoTransferStatus>> GetTransferStatusAsync(
            string? clientOrderId = null,
            string? transactionId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get account withdrawal status
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#retrieve-withdrawal-status" /></para>
        /// </summary>
        /// <param name="asset">Filter for a single asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoWithdrawStatus[]>> GetWithdrawStatusAsync(
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get saved addresses
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-saved-address" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="network">Filter by network</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#retrieve-personal-trading-fee" /></para>
        /// </summary>
        /// <param name="symbol">Specify a symbol to retrieve precise fee rate, for example `ETH_USDT`</param>
        /// <param name="settleAsset">Specify the settlement asset of the contract to get more accurate rate settings</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoFeeRate>> GetTradingFeeAsync(
            string? symbol = null,
            string? settleAsset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get account balance values
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#retrieve-user-s-total-balances" /></para>
        /// </summary>
        /// <param name="valuationAsset">Asset unit used to calculate the balance amount</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoAccountValuation>> GetAccountBalancesAsync(
           string? valuationAsset = null,
           CancellationToken ct = default);

        /// <summary>
        /// Get small balances
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#list-small-balance" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSmallBalance[]>> GetSmallBalancesAsync(
            CancellationToken ct = default);

        /// <summary>
        /// Convert small balances
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#convert-small-balance" /></para>
        /// </summary>
        /// <param name="assets">Assets to convert, for example `ETH`</param>
        /// <param name="all">Convert all</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ConvertSmallBalancesAsync(
            IEnumerable<string>? assets = null,
            bool? all = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get small balances conversion history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#list-small-balance-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSmallBalanceConversion[]>> GetSmallBalanceConversionsAsync(
            string? asset = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get unified account info
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-unified-account-information" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountInfo>> GetUnifiedAccountInfoAsync(
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get max borrowable amount
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-about-the-maximum-borrowing-for-the-unified-account" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMax>> GetUnifiedAccountBorrowableAsync(
            string asset,
            CancellationToken ct = default);

        /// <summary>
        /// Get max transferable amount
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-about-the-maximum-transferable-for-the-unified-account" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMax>> GetUnifiedAccountTransferableAsync(
            string asset,
            CancellationToken ct = default);

        /// <summary>
        /// Borrow or repay
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#borrow-or-repay" /></para>
        /// </summary>
        /// <param name="asset">Asset name, for example `ETH`</param>
        /// <param name="direction">Direction</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="repayAll">When set to 'true,' it overrides the 'amount,' allowing for direct full repayment.</param>
        /// <param name="text">User defined text</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-loans" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit</param>
        /// <param name="type">Loan type</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-load-records" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="direction">Direction</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-interest-records" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-user-risk-unit-details-only-valid-in-portfolio-margin-mode" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoRiskUnits>> GetUnifiedAccountRiskUnitsAsync(CancellationToken ct = default);

        /// <summary>
        /// Set unified account mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#set-mode-of-the-unified-account" /></para>
        /// </summary>
        /// <param name="mode">New mode</param>
        /// <param name="usdtFutures">USDT contract switch. This parameter is required when the mode is multi-currency margin mode</param>
        /// <param name="spotHedge">Spot hedging switch. This parameter is required when the mode is portfolio margin mode</param>
        /// <param name="useFunding">When the mode is set to combined margin mode, will funds be used as margin</param>
        /// <param name="options">Option switch. If not transmitted, the current switch value is used. If not transmitted for the first time, the default value is off</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetUnifiedAccountModeAsync(UnifiedAccountMode mode, bool? usdtFutures = null, bool? spotHedge = null, bool? useFunding = null, bool? options = null, CancellationToken ct = default);

        /// <summary>
        /// Get unified account mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-mode-of-the-unified-account" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMode>> GetUnifiedAccountModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get estimated lending rates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-unified-estimate-rate" /></para>
        /// </summary>
        /// <param name="assets">Up to 10 assets, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal?>>> GetUnifiedAccountEstimatedLendingRatesAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// Get unified account min and max leverage rates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#the-maximum-and-minimum-leverage-multiples-that-users-can-set-for-a-currency-type-are" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoLeverageConfig>> GetUnifiedLeverageConfigsAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the current leverage setttings
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-user-s-currency-leverage-if-currency-is-not-passed-query-all-currencies" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoLeverageSetting[]>> GetUnifiedLeverageAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Set the leverage for an asset
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-user-s-currency-leverage-if-currency-is-not-passed-query-all-currencies" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetUnifiedLeverageAsync(string asset, decimal leverage, CancellationToken ct = default);

        /// <summary>
        /// Get account and API key info
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-account-detail" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin account list
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#margin-account-list" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginAccount[]>> GetMarginAccountsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account list
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-user-s-isolated-margin-account-list" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoIsolatedMarginAccount[]>> GetIsolatedMarginAccountsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get margin accounts balance change history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-margin-account-balance-change-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#funding-account-list" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginFundingAccount[]>> GetMarginFundingAccountsAsync(
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Set auto repayment
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#update-user-s-auto-repayment-setting" /></para>
        /// </summary>
        /// <param name="enabled">True for auto repayment on, false for auto repayment off</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginAutoRepayStatus>> SetMarginAutoRepayAsync(
            bool enabled,
            CancellationToken ct = default);

        /// <summary>
        /// Get auto repayment setting
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-user-auto-repayment-setting" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginAutoRepayStatus>> GetMarginAutoRepayAsync(CancellationToken ct = default);

        /// <summary>
        /// Get max transferable quantity
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-max-transferable-amount-for-a-specific-margin-currency" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginMaxTransferable>> GetMarginMaxTransferableAsync(string asset, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-cross-margin-account" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginAccount>> GetCrossMarginAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-cross-margin-account-change-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-cross-margin-account-change-history" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="text">User defined text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan>> CreateCrossMarginLoanAsync(
            string asset,
            decimal quantity,
            string? text = null,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-cross-margin-borrow-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="reverse">Reverse results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-single-borrow-loan-detail" /></para>
        /// </summary>
        /// <param name="id">Loan id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan>> GetCrossMarginLoanAsync(string id,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-single-borrow-loan-detail" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginBorrowLoan[]>> CrossMarginRepayAsync(
            string asset,
            decimal quantity,
            CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-cross-margin-repayments" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="loanId">Loan id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="reverse">Reverse results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#interest-records-for-the-cross-margin-account" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-max-transferable-amount-for-a-specific-cross-margin-currency" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginMaxTransferable>> GetCrossMarginMaxTransferableAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#estimated-interest-rates" /></para>
        /// </summary>
        /// <param name="assets">Assets, max 10, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetCrossMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-max-borrowable-amount-for-a-specific-cross-margin-currency" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUnifiedAccountMax>> GetCrossMarginMaxBorrowableAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get margin estimated interest rates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#estimate-interest-rate" /></para>
        /// </summary>
        /// <param name="assets">Assets, max 10, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetMarginEstimatedInterestRatesAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// Borrow or repay margin loan
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#borrow-or-repay-2" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="direction">Borrow or repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="repayAll">Repay all instead of specifying quantity</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-loans-2" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-load-records-2" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="direction">Filter by direction</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-interest-records-2" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
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
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-maximum-borrowable" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoMarginMaxBorrowable>> GetMarginMaxBorrowableAsync(
            string asset,
            string symbol,
            CancellationToken ct = default);

        /// <summary>
        /// Get GT deduction enabled status
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#query-gt-deduction-configuration" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoGTDeducationStatus>> GetGTDeductionStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Set GT deduction enabled status
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#set-gt-deduction" /></para>
        /// </summary>
        /// <param name="enabled">Enabled</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetGTDeductionStatusAsync(bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Get transfer history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-the-uid-transfer-history" /></para>
        /// </summary>
        /// <param name="id">Filter by id</param>
        /// <param name="transactionType">Filter by transaction type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoTransferEntry[]>> GetTransferHistoryAsync(long? id = null, TransactionType? transactionType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer to another GateIo account
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#uid-transfer" /></para>
        /// </summary>
        /// <param name="receiveAccountId">Account id to transfer to</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoId>> TransferToAccountAsync(long receiveAccountId, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get rate limit ratios for the user
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-user-transaction-rate-limit-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoUserRateLimit[]>> GetRateLimitsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get insurance fund history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#query-spot-insurance-fund-historical-data" /></para>
        /// </summary>
        /// <param name="businessType">Business type</param>
        /// <param name="asset">Asset name</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
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
    }
}
