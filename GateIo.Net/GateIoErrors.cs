using CryptoExchange.Net.Objects.Errors;

namespace GateIo.Net
{
    internal static class GateIoErrors
    {
        internal static ErrorMapping RestErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API credentials", "INVALID_CREDENTIALS"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API key", "INVALID_KEY"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "IP_FORBIDDEN"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP Key is readonly", "READ_ONLY"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Account locked", "ACCOUNT_LOCKED"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "FORBIDDEN"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid signature", "INVALID_SIGNATURE"),

                new ErrorInfo(ErrorType.SystemError, true, "Internal server error", "INTERNAL", "SERVER_ERROR"),
                new ErrorInfo(ErrorType.SystemError, true, "Server too busy", "TOO_BUSY"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Timestamp expired", "REQUEST_EXPIRED"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter value", "INVALID_PARAM_VALUE", "INVALID_PROTOCOL", "INVALID_ARGUMENT"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid request body", "INVALID_REQUEST_BODY"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId invalid", "INVALID_CLIENT_ORDER_ID"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid decimal precision", "INVALID_PRECISION"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Too many different symbols in batch operation", "TOO_MANY_CURRENCY_PAIRS"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage too high", "LEVERAGE_TOO_HIGH"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage too low", "LEVERAGE_TOO_LOW"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Risk limit not multiple of step", "RISK_LIMIT_NOT_MULTIPLE"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Missing parameter", "MISSING_REQUIRED_PARAM"),

                new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too small", "QUANTITY_NOT_ENOUGH", "AMOUNT_TOO_LITTLE", "SIZE_TOO_SMALL"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too large", "AMOUNT_TOO_MUCH", "SIZE_TOO_LARGE"),

                new ErrorInfo(ErrorType.InvalidPrice, false, "Limit price too far from current price", "PRICE_TOO_DEVIATED"),

                new ErrorInfo(ErrorType.UnknownAsset, false, "Invalid asset", "INVALID_CURRENCY"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "INVALID_CURRENCY_PAIR", "CONTRACT_NOT_FOUND"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order not found", "ORDER_NOT_FOUND", "ORDER_NOT_OWNED"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "FUTURES_BALANCE_NOT_ENOUGH", "BALANCE_NOT_ENOUGH", "MARGIN_BALANCE_NOT_ENOUGH", "INSUFFICIENT_AVAILABLE"),

                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "PostOnly order would fill immediately", "POC_FILL_IMMEDIATELY", "ORDER_POC_IMMEDIATE"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "FillOrKill order would not fill immediately", "FOK_NOT_FILL", "ORDER_FOK"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Symbol is delisting, only reduceOnly and close orders are allowed", "CONTRACT_IN_DELISTING"),

                new ErrorInfo(ErrorType.IncorrectState, false, "Order already closed", "ORDER_CLOSED", "ORDER_FINISHED"),
                new ErrorInfo(ErrorType.IncorrectState, false, "Order already canceled", "ORDER_CANCELLED"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Withdraw request frequency exceeds limit", "TOO_FAST"),
                new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open orders", "TOO_MANY_ORDERS"),

                new ErrorInfo(ErrorType.NoPosition, false, "Position empty", "POSITION_EMPTY"),
                new ErrorInfo(ErrorType.NoPosition, false, "Position not found", "POSITION_NOT_FOUND"),

                new ErrorInfo(ErrorType.RiskError, false, "Risk limit exceeded", "RISK_LIMIT_EXCEEDED"),
                new ErrorInfo(ErrorType.RiskError, false, "Operation may cause liquidation", "LIQUIDATE_IMMEDIATELY"),
                new ErrorInfo(ErrorType.RiskError, false, "Risk limit too high", "RISK_LIMIT_TOO_HIGH"),
                new ErrorInfo(ErrorType.RiskError, false, "Risk limit too low", "RISK_LIMIT_TOO_lOW"),
            ]
        );

        internal static ErrorMapping SocketErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol", "2"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API key", "4"),
            ]
        );
    }
}