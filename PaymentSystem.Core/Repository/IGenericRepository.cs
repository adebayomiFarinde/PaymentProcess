using PaymentSystem.Core.Entities;
using System.Linq.Expressions;

namespace PaymentSystem.Core.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> where);
        Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> Query();
        Task Create(TEntity entity);
        Task Add(TEntity entity);
        Task AddRange(List<TEntity> entity);
        Task Update(TEntity entity);
        void UpdateRange(IList<TEntity> entities);
        Task Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task SoftDelete(TEntity entity);
        Task SaveChangesAsync();
    }
}
