using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Entities;
using Bank.Transfer.Infrastructure.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transfer.Infrastructure
{
    public static class MediatorExtension 
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, 
            BankContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.CleanEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }

    }
}
