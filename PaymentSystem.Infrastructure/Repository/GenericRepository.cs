using Microsoft.EntityFrameworkCore;
using PaymentSystem.Core.Entities;
using PaymentSystem.Core.Helper;
using PaymentSystem.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : Entity
    {
        private readonly DataContext _dbContext;

        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(where);
        }

        public async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>()
                .Where(where).ToListAsync();
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRange(List<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }
        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().Where(a => a.IsDeleted == false)
                    .OrderByDescending(a => a.CreatedDate);
        }

        public async Task Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return _dbContext.Set<TEntity>().Where(a => a.IsDeleted == false)
                .Where(where)
                .OrderByDescending(a => a.CreatedDate);
        }

        public async Task SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.UtcNow;
            _dbContext.Set<TEntity>().Update(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);

        }

        public void UpdateRange(IList<TEntity> entities)
        {
            entities = entities.ForEach(x => x.ModifiedDate = DateTime.UtcNow).ToList();
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }
    }
}
