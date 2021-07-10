using Bank.Transfer.Application.Interfaces;
using Bank.Transfer.Application.Queries;
using Bank.TransferRequest.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.TransferRequest.Application.Queries
{
    public class RequestStatusQueryHandler :
        IRequestHandler<RequestStatusQuery, RequestStatusDto>
    {
        private readonly ITransferenceAppService _transferenceAppService;

        public RequestStatusQueryHandler(ITransferenceAppService transferenceAppService)
        {
            _transferenceAppService = transferenceAppService;
        }
        public Task<RequestStatusDto> Handle(RequestStatusQuery request, CancellationToken cancellationToken)
        {
            var requestStatusDto = _transferenceAppService.GetRequestStatusById(request.TransactionId);
            return Task.FromResult(requestStatusDto);
        }
    }
}
