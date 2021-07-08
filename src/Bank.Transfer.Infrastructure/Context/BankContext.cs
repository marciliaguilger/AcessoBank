using Microsoft.EntityFrameworkCore;
using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Infrastructure.Mappings;
using Bank.Transfer.Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Messages;

namespace Bank.Transfer.Infrastructure.Context
{
    public class BankContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BankContext(DbContextOptions<BankContext> options, IMediatorHandler mediatorHandler) 
            : base(options) 
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Transference> Transfers { get; set; }

        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishEvents(this);

            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransferenceMap());
            modelBuilder.Ignore<Event>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
