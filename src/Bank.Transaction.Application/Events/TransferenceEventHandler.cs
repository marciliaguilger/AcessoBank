using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Transaction.Application.Events
{
    public class TransferenceEventHandler : 
        INotificationHandler<TransferenceNotFoundEvent>,
        INotificationHandler<InsuficientBalanceEvent>
    {
        public Task Handle(TransferenceNotFoundEvent notification, CancellationToken cancellationToken)
        {
            //TODO: ??
            throw new System.NotImplementedException();
        }

        public Task Handle(InsuficientBalanceEvent notification, CancellationToken cancellationToken)
        {
            //TODO: ??
            throw new System.NotImplementedException();
        }
    }
}
