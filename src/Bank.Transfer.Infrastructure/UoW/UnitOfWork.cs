using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Infrastructure.Context;
using System.Threading.Tasks;

namespace Bank.Transfer.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankContext _context;
        public UnitOfWork(BankContext context)
        {
            _context = context;
        }
        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
