using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
        Task<bool> UpdateAsync(TEntity obj);

        IUnitOfWork UnitOfWork { get; }
    }
}
