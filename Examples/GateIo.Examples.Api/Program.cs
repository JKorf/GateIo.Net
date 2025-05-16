using CryptoExchange.Net.Authentication;
using GateIo.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the GateIo services
builder.Services.AddGateIo();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddGateIo(options =>
{    
   options.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>");
   options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IGateIoRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickersAsync(symbol);
    return result.Data.First().LastPrice;
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IGateIoRestClient client) =>
{
    var result = await client.SpotApi.Account.GetBalancesAsync();
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();