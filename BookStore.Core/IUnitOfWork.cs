using BookStore.Core.Models;
using BookStore.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<TEntity, TKey>? Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        public Task<int> SaveChangesAsync();
    }
}
