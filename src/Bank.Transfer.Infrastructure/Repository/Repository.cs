using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transfer.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly BankContext Db;
        protected readonly DbSet<TEntity> DbSet;
        public Repository(BankContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
            
        }
        public IUnitOfWork UnitOfWork => Db;
        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
        }

        public void Dispose()
        {
            //Db.Dispose();
            //GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            Db.SaveChanges();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
            //Db.SaveChanges();
        }
        
        public async Task<bool> UpdateAsync(TEntity obj)
        {
            DbSet.Update(obj);
            return await Db.SaveChangesAsync() > 0;
        }
    }
}
