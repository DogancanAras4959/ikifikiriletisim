﻿using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.CORE.Repository
{
    public interface IRepositories<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query();
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(int id);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        IEnumerable<TEntity> Where(
            Expression<Func<TEntity, bool>> filterPredicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByPredicate = null,
            string navigationProperties = "",
            int? page = null,
            int? pageSize = null);
    }
}
