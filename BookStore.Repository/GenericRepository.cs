using BookStore.Core.Models;
using BookStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly BookStoreDbContext _context;

        public GenericRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTrack = true)
        {
            if (changeTrack)
                return await _context.Set<TEntity>()
                    .Where(e => !e.IsDeleted)
                    .ToListAsync();

            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .ToListAsync();

        }

        public async Task<TEntity> GetByIdAsync(TKey id)
            => await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => !e.IsDeleted && e.Id.Equals(id));

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);
 

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate, bool changeTrack = true)
        {
            if(changeTrack)
                return await _context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();

            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<TEntity> GetWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>()
                    .Where(predicate)
                    .FirstOrDefaultAsync();


        public async void HardDelete(TEntity entity)
            => _context.Set<TEntity>()
                .Remove(entity);

        public async Task HardDelete(TKey id)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<TEntity>()
                .Remove(entity);
        }

        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async Task SoftDelete(TKey id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            Update(entity);
        }
    }
}
