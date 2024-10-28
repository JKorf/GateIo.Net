
using GateIo.Net.Clients;

// REST
var restClient = new GateIoRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");
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

Console.ReadLine();
