using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication.Core.Data.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task InsertRangeAsync(IList<T> entities);
        Task RemoveRangeAsync(IList<T> entities);
        Task UpdateRangeAsync(IList<T> entities);

    }
}
