using GateIo.Net.Clients;
using GateIo.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using GateIo.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects.Errors;

namespace GateIo.Net.UnitTests
{
    [NonParallelizable]
    internal class GateIoRestIntegrationTests : RestIntegrationTest<GateIoRestClient>
    {
        public override bool Run { get; set; }

        public GateIoRestIntegrationTests()
        {
        }

        public override GateIoRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new GateIoRestClient(null, loggerFactory, Options.Create(new Objects.Options.GateIoRestOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new ApiCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Contains.Substring("INVALID_CURRENCY_PAIR"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            await RunAndCheckResult(client => client.SpotApi.Account.GetBalancesAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetLedgerAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalsAsync(default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositsAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawStatusAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetTradingFeeAsync(default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetAccountBalancesAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetSmallBalancesAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetSmallBalanceConversionsAsync(default, default, default, default), true);

            await RunAndCheckResult(client => client.SpotApi.Account.GetAccountInfoAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginAccountsAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginBalanceHistoryAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginFundingAccountsAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginAutoRepayAsync(default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetMarginMaxTransferableAsync("ETH", default, default), true);
            
            
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginEstimatedInterestRatesAsync(new[] { "ETH" }, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginLoansAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginLoanHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginInterestHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginMaxBorrowableAsync("ETH", "ETH_USDT", default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetGTDeductionStatusAsync(default), true);

            // Needs Unified account
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountInfoAsync(default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountBorrowableAsync("ETH", default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountTransferableAsync("ETH", default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountLoansAsync(default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountLoanHistoryAsync(default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountRiskUnitsAsync(default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetUnifiedAccountModeAsync(default), true);

            // Needs cross margin account
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginAccountsAsync(default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginBalanceHistoryAsync(default, default, default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginLoansAsync(default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginRepaymentsAsync(default, default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginInterestHistoryAsync(default, default, default, default, default, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginMaxTransferableAsync("ETH", default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginEstimatedInterestRatesAsync(new[] { "ETH" }, default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginMaxBorrowableAsync("ETH", default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetAsync("ETH", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolAsync("ETH_USDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync(default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETH_USDT", default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradesAsync("ETH_USDT", default, default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETH_USDT", GateIo.Net.Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetNetworksAsync("ETH", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetDiscountTiersAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLoanMarginTiersAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetCrossMarginAssetAsync("ETH", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetCrossMarginAssetsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLendingSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLendingSymbolAsync("ETH_USDT", default), false);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOrdersAsync(false, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetTriggerOrdersAsync(false, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestPerpetualFuturesAccount()
        {
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Account.GetAccountAsync("usdt", default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Account.GetLedgerAsync("usdt", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Account.GetTradingFeeAsync("usdt", default, default), true);
        }

        [Test]
        public async Task TestPerpetualFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetContractsAsync("usdt", default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetContractAsync("usdt", "ETH_USDT", default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetOrderBookAsync("usdt", "ETH_USDT", default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetTradesAsync("usdt", "ETH_USDT", default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetKlinesAsync("usdt", "ETH_USDT", GateIo.Net.Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetIndexKlinesAsync("usdt", "ETH_USDT", GateIo.Net.Enums.KlineInterval.FiveMinutes, default, default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetTickersAsync("usdt", default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetFundingRateHistoryAsync("usdt", "ETH_USDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetInsuranceBalanceHistoryAsync("usdt", default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetContractStatsAsync("usdt", "ETH_USDT", default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetIndexConstituentsAsync("usdt", "ETH_USDT", default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetLiquidationsAsync("usdt", default, default, default, default, default), false);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.ExchangeData.GetRiskLimitTiersAsync("usdt", "ETH_USDT", default, default, default), false);
        }

        [Test]
        public async Task TestPerpetualFuturesTrading()
        {
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetPositionsAsync("usdt", default, default,  default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetOrdersAsync("usdt", GateIo.Net.Enums.OrderStatus.Open, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetUserTradesAsync("usdt", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetPositionCloseHistoryAsync("usdt", default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetLiquidationHistoryAsync("usdt", default, default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetAutoDeleveragingHistoryAsync("usdt", default, default, default), true);
            await RunAndCheckResult(client => client.PerpetualFuturesApi.Trading.GetTriggerOrdersAsync("usdt", false, default, default, default, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new GateIoSpotSymbolOrderBook("ETH_USDT"));
            await TestOrderBook(new GateIoPerpetualFuturesSymbolOrderBook("usdt", "ETH_USDT"));
        }

    }
}
