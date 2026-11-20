# Operational considerations

## Credentials, settlement, and account scope

Private REST/socket operations use `GateIoCredentials(key, secret)` without a passphrase. Keep credentials outside source and grant permissions for the intended API area. Clients default to `Live`; select `Demo` explicitly with matching credentials for demo workflows.

Futures settlement asset is separate from contract name. Keep settlement routing consistent for metadata, balances, orders, and positions. Private futures subscriptions need the user ID matching the credentials; shared requests also carry the required `SettleAsset` and `UserId` exchange parameters.

## Spot execution and account types

Spot limit quantity and market-sell quantity are in base asset; spot market-buy quantity is in quote asset. Validate minimums, increments, price precision, and account type. Margin, unified borrowing/repayment, auto-borrow, and auto-repay settings can change liabilities and collateral behavior. Trigger orders have their own account/trigger parameters.

Limit orders can fill immediately when their price crosses the book. Cancellation can race with a fill; reconcile order/trade state instead of assuming acceptance of cancellation proves no execution occurred.

## Perpetual futures

Current native placement accepts decimal contract quantities. Use contract multiplier and size constraints to determine valid size and exposure; do not treat a contract count as a base-asset amount or assume all contracts permit arbitrary fractions.

`price: 0m` with `TimeInForce.ImmediateOrCancel` is the native market-style futures pattern. Leverage, margin mode, risk limits, position mode, and collateral updates can persist and affect existing exposure. Match single/dual position mode and intended position side. Use the supported reduce-only/close-side semantics when reducing positions, and preserve symbol/settlement scope for broad cancellations.

## Batch results and uncertain submissions

Spot batch operations can return `GateIoOrderOperation[]`: inspect each item's `Succeeded`, `ErrorCode`, and `ErrorMessage` after the outer request succeeds. Futures batch responses carry per-order outcomes such as nullable `Succeeded` and error details; inspect them rather than assuming the full batch succeeded.

A timeout or lost response can occur after acceptance of an order, cancellation, transfer, withdrawal, or settings change. Query supported order/client identifiers and relevant account/transaction state before deciding whether to resubmit. Transient errors do not make account-changing requests safe to repeat; bound public-read retries separately from submission recovery.

## Fund movements and specialized products

Transfers, withdrawals, unified/margin borrowing, Alpha orders, and conversions change funds or obligations. Preserve source/destination account, settlement asset where applicable, asset, network, address, required memo/tag, and amount. Request acceptance is distinct from completed settlement. Alpha quote and execution workflows use their own native request models rather than ordinary spot order parameters.
