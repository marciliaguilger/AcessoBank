using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        
        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public IQueryable GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
        
        public async Task<bool> UpdateAsync(TEntity obj)
        {
            return await _repository.UpdateAsync(obj);
        }

        public async Task<bool> Commit()
        {
            return await _repository.UnitOfWork.Commit();
        }

    }
}
