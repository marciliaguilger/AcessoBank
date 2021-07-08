using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork 
    {
        Task<bool> Commit();
    }
}

