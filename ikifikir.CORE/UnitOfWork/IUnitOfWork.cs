using ikifikir.CORE.Repository;
using ikifikir.DAL.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.CORE.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IRepositories<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        Task<int> Commit();
        void RollBack();
    }
}
