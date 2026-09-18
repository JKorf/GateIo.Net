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

        #region Get Withdrawal History

        async Task<IExchangeCallResult<SharedWithdrawal[]>> IGetWithdrawalHistory.GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetWithdrawalHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(30));

            // Get data
            var result = await _api.Account.GetWithdrawalsAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

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
                        new SharedWithdrawal(
                            x.Asset,
                            x.Address,
                            x.Quantity,
                            x.Status == WithdrawalStatus.Done,
                            x.Timestamp,
                            GetWithdrawalStatus(x))
                        {
                            Network = x.Network,
                            Tag = x.Memo,
                            TransactionId = x.TransactionId,
                            Fee = x.Fee
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus GetWithdrawalStatus(GateIoWithdrawal x)
        {
            if (x.Status == WithdrawalStatus.Blocked
                || x.Status == WithdrawalStatus.Canceled
                || x.Status == WithdrawalStatus.FailedConfirmation
                || x.Status == WithdrawalStatus.Invalid)
            {
                return SharedTransferStatus.Failed;
            }

            if (x.Status == WithdrawalStatus.Done
                || x.Status == WithdrawalStatus.Credited
                || x.Status == WithdrawalStatus.Final)
            {
                return SharedTransferStatus.Completed;
            }

            if (x.Status == WithdrawalStatus.Pending
                || x.Status == WithdrawalStatus.PendingApproval
                || x.Status == WithdrawalStatus.PendingConfirmation
                || x.Status == WithdrawalStatus.Processing
                || x.Status == WithdrawalStatus.Requested
                || x.Status == WithdrawalStatus.RequiresManualApproval
                || x.Status == WithdrawalStatus.Review
                || x.Status == WithdrawalStatus.Track
                || x.Status == WithdrawalStatus.Verifying)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.Unknown;
        }


        #region Withdraw

        async Task<IExchangeCallResult<SharedId>> IWithdraw.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
            => await WithdrawAsync(request, ct).ConfigureAwait(false);

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<WithdrawRequest>.Required(x => x.Network)
            ]
        };
        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                address: request.Address,
                quantity: request.Quantity,
                network: request.Network!,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.Id));
        }

        #endregion

    }
}
