using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication.Core.Data.Interfaces;
using WebApplication.Core.Domain;

namespace WebApplication.Core.Data.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly WebApplicationContext _dataContext;

        public Repository(WebApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }


        public DbSet<T> Set()
        {
            return _dataContext.Set<T>();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _dataContext.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task InsertRangeAsync(IList<T> entities)
        {
            await _dataContext.AddRangeAsync(entities);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dataContext.Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }


        public async Task UpdateRangeAsync(IList<T> entities)
        {
            _dataContext.UpdateRange(entities);
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IList<T> entities)
        {
            _dataContext.RemoveRange(entities);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return Set().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Set().SingleOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties);
        }

        public IQueryable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IQueryable<T> results = query.Where(predicate);
            return results;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> results = Set().AsNoTracking().Where(predicate);
            return results;
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = Set();

            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking();
        }

    }

}
