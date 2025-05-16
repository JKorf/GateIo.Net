using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Objects.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gate.io.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new GateIoRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<GateIoRestClient>(client, "Endpoints/Spot/Account", "https://api.gateio.ws", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetLedgerAsync(), "GetLedger");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("ETH", 1, "123", "ETH"), "Withdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.CancelWithdrawalAsync("123"), "CancelWithdrawal");
            await tester.ValidateAsync(client => client.SpotApi.Account.GenerateDepositAddressAsync("ETH"), "GenerateDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalsAsync(), "GetWithdrawals");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositsAsync(), "GetDeposits");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync("ETH", AccountType.Spot, AccountType.PerpertualFutures, 1), "Transfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawStatusAsync(), "GetWithdrawStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSavedAddressAsync("ETH"), "GetSavedAddress", ignoreProperties: new List<string> { "verified" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTradingFeeAsync(), "GetTradingFee", ignoreProperties: new List<string> { "point_type" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountBalancesAsync(), "GetAccountBalances");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSmallBalancesAsync(), "GetSmallBalances");
            await tester.ValidateAsync(client => client.SpotApi.Account.ConvertSmallBalancesAsync(), "ConvertSmallBalances");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSmallBalanceConversionsAsync(), "GetSmallBalanceConversions");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountInfoAsync(), "GetUnifiedAccountInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountBorrowableAsync("ETH"), "GetUnifiedAccountBorrowable");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountTransferableAsync("ETH"), "GetUnifiedAccountTransferable");
            await tester.ValidateAsync(client => client.SpotApi.Account.UnifiedAccountBorrowOrRepayAsync("ETH", BorrowDirection.Borrow, 10), "BorrowOrRepay");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountLoansAsync("ETH"), "GetLoans");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountLoanHistoryAsync(), "GetLoanHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountInterestHistoryAsync(), "GetInterestHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountRiskUnitsAsync(), "GetRiskUnits");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetUnifiedAccountModeAsync(UnifiedAccountMode.Classic), "SetUnifiedAccountMode");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountModeAsync(), "GetUnifiedAccountMode");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedAccountEstimatedLendingRatesAsync(new[] { "ETH" }), "GetEstimatedLendingRates");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountInfoAsync(), "GetAccountInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginAccountsAsync(), "GetMarginAccounts");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginBalanceHistoryAsync(), "GetMarginBalanceHistory", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginFundingAccountsAsync(), "GetMarginFundingAccounts");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginFundingAccountsAsync(), "GetMarginFundingAccounts");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetMarginAutoRepayAsync(true), "SetMarginAutoRepay");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginMaxTransferableAsync("ETH"), "GetMarginMaxTransferable");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginAccountsAsync(), "GetCrossMarginAccounts");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginBalanceHistoryAsync(), "GetCrossMarginBalanceHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.CreateCrossMarginLoanAsync("ETH", 1), "CreateCrossMarginLoan");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginLoansAsync("ETH"), "GetCrossMarginLoans");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginLoanAsync("123"), "GetCrossMarginLoan");
            await tester.ValidateAsync(client => client.SpotApi.Account.CrossMarginRepayAsync("ETH", 123), "CrossMarginRepay");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginRepaymentsAsync("ETH"), "GetCrossMarginRepayments");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginInterestHistoryAsync("ETH"), "GetCrossMarginInterestHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginMaxTransferableAsync("ETH"), "GetCrossMarginMaxTransferable");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginEstimatedInterestRatesAsync(new[] { "ETH" }), "GetCrossMarginEstimatedInterestRates");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginMaxBorrowableAsync("ETH"), "GetCrossMarginMaxBorrowable");
            await tester.ValidateAsync(client => client.SpotApi.Account.BorrowOrRepayAsync("ETH", "ETH_USDT", BorrowDirection.Borrow, 1), "MarginBorrowOrRepay");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginLoansAsync(), "GetMarginLoans");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginLoanHistoryAsync(), "GetMarginLoanHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginInterestHistoryAsync(), "GetMarginInterestHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginMaxBorrowableAsync("ETH", "ETH_USDT"), "GetMarginMaxBorrowable");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetGTDeductionStatusAsync(), "GetGTDeductionStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetGTDeductionStatusAsync(true), "SetGTDeductionStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferToAccountAsync(123, "123", 0.1m), "TransferToAccount");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetRateLimitsAsync(), "GetRateLimits");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedLeverageConfigsAsync("123"), "GetUnifiedLeverageConfigs"); ;
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUnifiedLeverageAsync(), "GetUnifiedLeverage");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetUnifiedLeverageAsync("123", 0.1m), "SetUnifiedLeverage");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new GateIoRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<GateIoRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.gateio.ws", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetAsync("BTC"), "GetAsset");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolAsync("BTC_USDT"), "GetSymbol");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolsAsync(), "GetSymbols");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("BTC_USDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradesAsync("BTC_USDT"), "GetTrades", ignoreProperties: new List<string> { "create_time" });
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("BTC_USDT", KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetNetworksAsync("BTC"), "GetNetworks");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetDiscountTiersAsync(), "GetDiscountTiers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLoanMarginTiersAsync(), "GetLoanMarginTiers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetCrossMarginAssetAsync("ETH"), "GetCrossMarginAsset");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetCrossMarginAssetsAsync(), "GetCrossMarginAssets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLendingSymbolsAsync(), "GetLendingSymbols");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLendingSymbolAsync("ETH"), "GetLendingSymbol");

        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new GateIoRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<GateIoRestClient>(client, "Endpoints/Spot/Trading", "https://api.gateio.ws", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETH_USDT", OrderSide.Buy, NewOrderType.Limit, 40), "PlaceOrder", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price", "" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync(true, "ETH_USDT"), "GetOrders", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("ETH_USDT", 123), "GetOrder", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync("ETH_USDT"), "CancelAllOders", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrdersAsync(new[] { new GateIoBatchCancelRequest { Id = "123", Symbol = "ETH_USDT" } }), "CancelOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.EditOrderAsync("ETH_USDT", 123, price: 123), "EditOrder", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync("ETH_USDT", 123), "CancelOrder", ignoreProperties: new List<string> { "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync(), "GetUserTrades", ignoreProperties: new List<string> { "create_time" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrdersAfterAsync(TimeSpan.Zero), "CancelOrdersAfter");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceTriggerOrderAsync("ETH_USDT", OrderSide.Sell, NewOrderType.Market, TriggerType.EqualOrHigher, 2000, TimeSpan.FromMinutes(10), 10, TriggerAccountType.Normal, TimeInForce.GoodTillCancel), "PlaceTriggerOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetTriggerOrdersAsync(false), "GetTriggerOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllTriggerOrdersAsync(), "CancelAllTriggerOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetTriggerOrderAsync(123), "GetTriggerOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelTriggerOrderAsync(123), "CancelTriggerOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMultipleOrderAsync(new[] { new GateIoBatchPlaceRequest() }), "PlaceMultipleOrder", ignoreProperties: new List<string> { "order_id", "create_time", "update_time", "fill_price" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.EditMultipleOrderAsync(new[] { new GateIoBatchEditRequest() }), "EditMultipleOrder", ignoreProperties: new List<string> { "order_id", "create_time", "update_time", "fill_price" });

        }

        [Test]
        public async Task ValidatePerpFuturesAccountDataCalls()
        {
            var client = new GateIoRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<GateIoRestClient>(client, "Endpoints/PerpetualFutures/Account", "https://api.gateio.ws", IsAuthenticated);
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Account.GetAccountAsync("usdt"), "GetAccount");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Account.GetLedgerAsync("usdt"), "GetLedger");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Account.UpdatePositionModeAsync("usdt", true), "UpdatePositionMode");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Account.GetTradingFeeAsync("usdt"), "GetTradingFee");
        }

        [Test]
        public async Task ValidatePerpFuturesExchangeDataCalls()
        {
            var client = new GateIoRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<GateIoRestClient>(client, "Endpoints/PerpetualFutures/ExchangeData", "https://api.gateio.ws", IsAuthenticated);
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetContractsAsync("usdt"), "GetContracts", ignoreProperties: new List<string> { "risk_limit_base", "risk_limit_step", "risk_limit_max" });
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetContractAsync("usdt", "ETH_USDT"), "GetContract", ignoreProperties: new List<string> { "risk_limit_base", "risk_limit_step", "risk_limit_max" });
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetOrderBookAsync("usdt", "ETH_USDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetTradesAsync("usdt", "ETH_USDT"), "GetTrades");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetKlinesAsync("usdt", "ETH_USDT", KlineInterval.EightHours), "GetKlines");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetIndexKlinesAsync("usdt", "ETH_USDT", KlineInterval.EightHours), "GetIndexKlines");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetTickersAsync("usdt", "ETH_USDT"), "GetTickers");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetFundingRateHistoryAsync("usdt", "ETH_USDT"), "GetFundingRateHistory");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetInsuranceBalanceHistoryAsync("usdt"), "GetInsuranceBalanceHistory");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetContractStatsAsync("usdt", "ETH_USDT"), "GetContractStats");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetIndexConstituentsAsync("usdt", "ETH_USDT"), "GetIndexConstituents");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetLiquidationsAsync("usdt", "ETH_USDT"), "GetLiquidations");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.ExchangeData.GetRiskLimitTiersAsync("usdt", "ETH_USDT"), "GetRiskLimitTiers");

        }

        [Test]
        public async Task ValidatePerpFuturesTradingDataCalls()
        {
            var client = new GateIoRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<GateIoRestClient>(client, "Endpoints/PerpetualFutures/Trading", "https://api.gateio.ws", IsAuthenticated);
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetPositionsAsync("usdt"), "GetPositions");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.UpdatePositionMarginAsync("usdt", "ETH_USDT", 1), "UpdatePositionMargin");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.UpdatePositionLeverageAsync("usdt", "ETH_USDT", 1), "UpdatePositionLeverage");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.UpdatePositionRiskLimitAsync("usdt", "ETH_USDT", 1), "UpdatePositionRiskLimit");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetDualModePositionsAsync("usdt", "ETH_USDT"), "GetDualModePositions");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.UpdateDualModePositionMarginAsync("usdt", "ETH_USDT", 1, PositionMode.Single), "UpdateDualModePositionMargin");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.UpdateDualModePositionLeverageAsync("usdt", "ETH_USDT", 10), "UpdateDualModePositionLeverage");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.UpdateDualModePositionRiskLimitAsync("usdt", "ETH_USDT", 10), "UpdateDualModePositionRiskLimit");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.PlaceOrderAsync("usdt", "ETH_USDT", OrderSide.Buy, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.PlaceMultipleOrderAsync("usdt", new[] { new GateIoPerpBatchPlaceRequest() }), "PlaceMultipleOrder");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetOrdersAsync("usdt", OrderStatus.Canceled), "GetOrders");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetOrdersByTimestampAsync("usdt", "ETH_USDT"), "GetOrdersByTime");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.CancelAllOrdersAsync("usdt", "ETH_USDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetOrderAsync("usdt", 123), "GetOrder");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.CancelOrderAsync("usdt", 123), "CancelOrder");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.EditOrderAsync("usdt", 123), "EditOrder");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetUserTradesAsync("usdt"), "GetUserTrades");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetUserTradesByTimestampAsync("usdt"), "GetUserTradesByTimestamp");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetPositionCloseHistoryAsync("usdt"), "GetPositionCloseHistory");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetLiquidationHistoryAsync("usdt"), "GetLiquidationHistory");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetAutoDeleveragingHistoryAsync("usdt"), "GetAutoDeleveragingHistory");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.CancelOrdersAfterAsync("usdt", TimeSpan.Zero), "CancelOrdersAfter");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.CancelOrdersAsync("usdt", new[] { 1L }), "CancelOrders");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.PlaceTriggerOrderAsync("usdt", "ETH_USDT", OrderSide.Sell, 1, TriggerType.EqualOrLower, 100), "PlaceTriggerOrder");
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetTriggerOrdersAsync("usdt", true), "GetTriggerOrders", ignoreProperties: new List<string> { "strategy_type" });
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.CancelTriggerOrdersAsync("usdt", "ETH_USDT"), "CancelTriggerOrders", ignoreProperties: new List<string> { "strategy_type" });
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.GetTriggerOrderAsync("usdt", 123), "GetTriggerOrder", ignoreProperties: new List<string> { "strategy_type" });
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.CancelTriggerOrderAsync("usdt", 123), "CancelTriggerOrder", ignoreProperties: new List<string> { "strategy_type" });
            await tester.ValidateAsync(client => client.PerpetualFuturesApi.Trading.EditMultipleOrdersAsync("usdt", new[] { new GateIoPerpBatchEditRequest() }), "EditMultipleOrders");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(r => r.Key == "SIGN");
        }
    }
}
