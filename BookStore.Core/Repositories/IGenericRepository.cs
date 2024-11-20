using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool changeTrack = true);
        Task<IEnumerable<TEntity>> GetAllWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate, bool changeTrack = true);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void HardDelete(TEntity entity);
        Task HardDelete(TKey id);
        void SoftDelete(TEntity entity);
        Task SoftDelete(TKey id);
    }
}
