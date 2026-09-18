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

        #region Get Deposit Addresses

        async Task<IExchangeCallResult<SharedDepositAddress[]>> IGetDepositAddresses.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
            => await GetDepositAddressesAsync(request, ct).ConfigureAwait(false);

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetDepositHistoryAsync(request, pageRequest, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GenerateDepositAddressAsync(request.Asset).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.MultichainAddress.Where(x => string.IsNullOrEmpty(request.Network) ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(depositAddresses.Data.Asset, x.Address)
                {
                    Network = x.Network,
                    TagOrMemo = x.PaymentId
                }
            ).ToArray());
        }

        #endregion

        #region Get Deposit History

        async Task<IExchangeCallResult<SharedDeposit[]>> IGetDepositHistory.GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetDepositHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 500);
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(30));

            // Get data
            var result = await _api.Account.GetDepositsAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(30));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == WithdrawalStatus.Done,
                            x.Timestamp,
                            ParseTransferStatus(x.Status))
                        {
                            Network = x.Network,
                            TransactionId = x.TransactionId,
                            Tag = x.Memo
                        }).ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus ParseTransferStatus(WithdrawalStatus status)
        {
            if (status == WithdrawalStatus.Done || status == WithdrawalStatus.Final || status == WithdrawalStatus.Credited)
                return SharedTransferStatus.Completed;
            if (status == WithdrawalStatus.Blocked || status == WithdrawalStatus.Invalid || status == WithdrawalStatus.FailedConfirmation || status == WithdrawalStatus.Canceled)
                return SharedTransferStatus.Failed;
            if (status == WithdrawalStatus.Processing
                || status == WithdrawalStatus.Verifying
                || status == WithdrawalStatus.Track
                || status == WithdrawalStatus.RequiresManualApproval
                || status == WithdrawalStatus.Requested
                || status == WithdrawalStatus.PendingConfirmation
                || status == WithdrawalStatus.PendingApproval
                || status == WithdrawalStatus.Pending
                || status == WithdrawalStatus.GateCode
                || status == WithdrawalStatus.Review)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.Unknown;
        }

    }
}
