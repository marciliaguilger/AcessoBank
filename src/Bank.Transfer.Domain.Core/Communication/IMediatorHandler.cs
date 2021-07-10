using Bank.Transfer.Domain.Core.Messages;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Core.Communication
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;

        //Task<bool> SendCommand<T>(T command) where T : Command;
        Task<R> SendCommand<T, R>(T command) where T : Command<R>;
        //Task<R> SendCommandWithReturn<T, R>(T command);
        //Task<R> GetQuery<T, R>(T query);
    }
}
