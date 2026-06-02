
using GateIo.Net.Clients;

// REST
var restClient = new GateIoRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.First().LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new GateIoSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETH_USDT: {update.Data.LastPrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
