using Atm.Fornecedor.Domain;
using Atm.Fornecedor.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Dados.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IDbContext _context;

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddCollectionAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> lambda)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(params Expression<Func<T, object>>[] joins)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstAsync(Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void SetInsertData(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCollectionAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
