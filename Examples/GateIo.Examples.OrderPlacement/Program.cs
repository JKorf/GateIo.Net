using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Enums;

const string spotSymbol = "BTC_USDT";
const string futuresSettlementAsset = "usdt";
const string futuresContract = "ETH_USDT";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";

Console.WriteLine("GateIo.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(GateIoRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var tickers = await client.SpotApi.ExchangeData.GetTickersAsync(spotSymbol);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.FirstOrDefault();
    if (ticker == null)
    {
        Console.WriteLine("Failed to get spot ticker: no ticker returned");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: NewOrderType.Limit,
        quantity: 0.001m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCancel);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.Id}, status: {order.Data.Status}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(spotSymbol, order.Data.Id);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(spotSymbol, order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.Id}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesReduceOnlyOrderExampleAsync(GateIoRestClient client)
{
    Console.WriteLine($"Placing futures reduce-only limit sell order for {futuresContract}...");

    var tickers = await client.PerpetualFuturesApi.ExchangeData.GetTickersAsync(futuresSettlementAsset, futuresContract);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.FirstOrDefault();
    if (ticker == null)
    {
        Console.WriteLine("Failed to get futures ticker: no ticker returned");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice * 1.05m, 2);
    var order = await client.PerpetualFuturesApi.Trading.PlaceOrderAsync(
        settlementAsset: futuresSettlementAsset,
        contract: futuresContract,
        orderSide: OrderSide.Sell,
        quantity: 1,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCancel,
        reduceOnly: true);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.Id}, status: {order.Data.Status}");

    var orderStatus = await client.PerpetualFuturesApi.Trading.GetOrderAsync(futuresSettlementAsset, order.Data.Id);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, remaining: {orderStatus.Data.QuantityRemaining}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.PerpetualFuturesApi.Trading.CancelOrderAsync(futuresSettlementAsset, order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.Id}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
