using Microsoft.EntityFrameworkCore;
using RecCore.Contracts.Repositories;
using RecCore.Entities;
using RecInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecInfrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly RecDbContext _dbContext;
        public BaseRepository(RecDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> DeleteAsync(int id)
        {
            T t = await GetByIdAsync(id);
            if (t != null)
            {
                _dbContext.Set<T>().Remove(t);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAndIncludesAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var navigationProp in includes)
                {
                    query = query.Include(navigationProp);
                }
            }
            return await query.Where(filter).ToListAsync();
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null) return false;
            return await _dbContext.Set<T>().Where(filter).AnyAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
