using Bank.Transfer.Domain.Core.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MediatorHandler> _logger;

        public MediatorHandler(IMediator mediator, ILogger<MediatorHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
