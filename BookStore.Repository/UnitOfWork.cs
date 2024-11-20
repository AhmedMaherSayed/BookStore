using BookStore.Core;
using BookStore.Core.Models;
using BookStore.Core.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(BookStoreDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

        public IGenericRepository<TEntity, TKey>? Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                _repositories.TryAdd(type, new GenericRepository<TEntity, TKey>(_context));
            }
            return _repositories[type] as IGenericRepository<TEntity, TKey>;
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
