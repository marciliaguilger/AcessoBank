using Bank.Transfer.Domain.Core.Messages;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Core.Communication
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;

        Task<R> SendCommand<T, R>(T command) where T : Command<R>;
    }
}
