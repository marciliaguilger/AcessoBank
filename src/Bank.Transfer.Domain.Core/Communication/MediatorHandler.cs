using Bank.Transfer.Domain.Core.Messages;
using MediatR;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
        public async Task<R> SendCommand<T, R>(T command) where T : Command<R>
        {
            return await _mediator.Send(command);
        }
    }
}
