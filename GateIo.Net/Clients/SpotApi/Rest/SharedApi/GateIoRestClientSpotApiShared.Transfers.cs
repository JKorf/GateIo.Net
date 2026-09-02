using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Objects.Models;
using CryptoExchange.Net;

namespace GateIo.Net.Clients.SpotApi
{
    internal partial class GateIoRestClientSpotSharedApi
    {
        #region Transfer client

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Spot,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.Option,
            ])
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "The settle asset for futures transfer", "usdt")
            }
        };
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await _api.Account.TransferAsync(
                request.Asset,
                fromType.Value,
                toType.Value,
                request.Quantity,
                fromType == AccountType.Margin ? request.FromSymbol : request.ToSymbol,
                ExchangeParameters.GetValue<string?>(request.ExchangeParameters, Exchange, "SettleAsset"),
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data.TransactionId.ToString()));
        }

        private AccountType? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Spot) return AccountType.Spot;
            if (type.IsMarginAccount()) return AccountType.Margin;
            if (type == SharedAccountType.PerpetualLinearFutures || type == SharedAccountType.PerpetualInverseFutures) return AccountType.PerpertualFutures;
            if (type == SharedAccountType.DeliveryLinearFutures || type == SharedAccountType.DeliveryInverseFutures) return AccountType.DeliveryFutures;
            if (type == SharedAccountType.Option) return AccountType.Options;
            return null;
        }

        #endregion
    }
}
