# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package GateIo.Net` and it builds (you only need to substitute API keys for trading examples).
- **Self-contained** - single file, no external setup, no shared helpers.
- **Heavily commented** - explains why each line, not just what.
- **Idiomatic** - follows current GateIo.Net 3.x patterns.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Client setup, public ticker, authenticated balances, place limit order, query order status |
| `02-futures.cs` | Perpetual futures: settlement asset, contract metadata, set leverage, place market-style order, get position, close position |
| `03-websocket.cs` | Subscribe to ticker updates, klines, authenticated spot streams, futures ticker stream, with proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `WebCallResult` patterns, retry, common Gate.io routing and validation issues |

## Running

```bash
dotnet new console -n MyGateIoApp
cd MyGateIoApp
dotnet add package GateIo.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET placeholders with your own
dotnet run
```
