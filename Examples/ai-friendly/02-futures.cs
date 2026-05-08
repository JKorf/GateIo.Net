// 02-futures.cs
//
// Demonstrates: Gate.io perpetual futures - settlement asset, contract metadata,
// set leverage, place an order, retrieve open position, close position.
//
// Setup: dotnet add package GateIo.Net
// Substitute API_KEY / API_SECRET. The API key must have futures trading enabled.

using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Objects;

var client = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});

const string settlementAsset = "usdt";
const string contract = "ETH_USDT";

// ---- 1. INSPECT CONTRACT METADATA ----
// Futures quantity is an integer number of contracts, not a decimal ETH amount.
// Use contract metadata to understand multiplier, allowed size, and price precision.
var contractInfo = await client.PerpetualFuturesApi.ExchangeData.GetContractAsync(settlementAsset, contract);
if (!contractInfo.Success)
{
    Console.WriteLine($"Failed to get contract info: {contractInfo.Error}");
    return;
}

Console.WriteLine($"{contract} multiplier: {contractInfo.Data.Multiplier}");

// ---- 2. SET LEVERAGE ----
// Leverage is contract-specific and persists until changed.
var leverage = await client.PerpetualFuturesApi.Trading.UpdatePositionLeverageAsync(
    settlementAsset,
    contract,
    leverage: 5);

if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}

Console.WriteLine($"Leverage set to {leverage.Data.Leverage}x for {contract}");

// ---- 3. PLACE MARKET-STYLE ORDER (open long position) ----
// Gate.io futures PlaceOrderAsync uses an int contract quantity.
// price: 0m with ImmediateOrCancel is the Gate.io market-style futures pattern.
var openOrder = await client.PerpetualFuturesApi.Trading.PlaceOrderAsync(
    settlementAsset: settlementAsset,
    contract: contract,
    orderSide: OrderSide.Buy,
    quantity: 1,
    price: 0m,
    timeInForce: TimeInForce.ImmediateOrCancel);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}

Console.WriteLine($"Opened position via order {openOrder.Data.Id}");

// ---- 4. GET CURRENT POSITION ----
var position = await client.PerpetualFuturesApi.Trading.GetPositionAsync(settlementAsset, contract);
if (!position.Success)
{
    Console.WriteLine($"Failed to get position: {position.Error}");
    return;
}

if (position.Data.Size == 0)
{
    Console.WriteLine("No open position found (the order may not have filled yet).");
    return;
}

Console.WriteLine($"Position: {position.Data.Size} contracts {position.Data.Contract}");
Console.WriteLine($"Entry price: {position.Data.EntryPrice}");
Console.WriteLine($"Leverage: {position.Data.Leverage}x");

// ---- 5. CLOSE THE POSITION ----
// Opposite side, same absolute contract size, reduceOnly=true to avoid flipping position.
var closeOrder = await client.PerpetualFuturesApi.Trading.PlaceOrderAsync(
    settlementAsset: settlementAsset,
    contract: contract,
    orderSide: position.Data.Size > 0 ? OrderSide.Sell : OrderSide.Buy,
    quantity: (int)Math.Abs(position.Data.Size),
    price: 0m,
    reduceOnly: true,
    timeInForce: TimeInForce.ImmediateOrCancel);

if (closeOrder.Success)
{
    Console.WriteLine($"Closed position via order {closeOrder.Data.Id}");
}

// Common variations:
//   Limit order:          provide price + timeInForce: TimeInForce.GoodTillCancel
//   Isolated margin mode: client.PerpetualFuturesApi.Account.SetMarginModeAsync(settlementAsset, contract, MarginMode.Isolated)
//   Dual position mode:   client.PerpetualFuturesApi.Account.UpdatePositionModeAsync(settlementAsset, dualMode: true)
//   Trigger order:        client.PerpetualFuturesApi.Trading.PlaceTriggerOrderAsync(...)
//   Other settlement:     use "btc" or "usd" instead of "usdt" where the contract exists
