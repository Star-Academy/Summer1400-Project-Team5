﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Talent.Services.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderedBy = null,
            List<string> includes = null
        );

        Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);

        Task DeleteAsync(int id);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}
