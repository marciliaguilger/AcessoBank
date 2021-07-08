using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Interfaces.Service
{
    public interface IService<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        void Remove(Guid id);
        IQueryable GetAll ();
        TEntity GetById(Guid id);

        Task<bool> Commit();
    }
}
