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

        public async Task<R> GetQuery<T, R>(T query)
        {
            return (R)await _mediator.Send(query);
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
