using BookStore.Core.Models;
using BookStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!changeTrack)
                return await _context.Set<TEntity>().ToListAsync();

            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity> GetByIdAsync(TKey id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public async void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

       

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
    }
}
