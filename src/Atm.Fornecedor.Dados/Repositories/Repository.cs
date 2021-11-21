﻿using Atm.Fornecedor.Domain;
using Atm.Fornecedor.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Dados.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IDbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        protected IQueryable<T> Query(params Expression<Func<T, object>>[] joins)
        {
            var query = _context.Set<T>()
                                .AsQueryable();
            return joins == null ? query : joins.Aggregate(query, (current, include) => current.Include(include));
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>()
                          .AddAsync(SetAddData(entity))
                          .ConfigureAwait(false);
        }

        private T SetAddData(T entity)
        {
            SetInsertData(entity);
            return entity;
        }

        public virtual async Task AddCollectionAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>()
                          .AddRangeAsync(SetAddData(entities))
                          .ConfigureAwait(false);
        }

        private IEnumerable<T> SetAddData(IEnumerable<T> entities)
        {
            return entities.Select(entity => { return SetAddData(entity); });
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> lambda)
        {
            return await Query().AnyAsync(lambda);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(params Expression<Func<T, object>>[] joins)
        {
            return await Query(joins).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins)
        {
            return await Query(joins).Where(lambda).ToListAsync();
        }

        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins)
        {
            return await Query(joins).FirstOrDefaultAsync(lambda);
        }

        public virtual Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void SetInsertData(Entity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DataCadastro = DateTime.Now;
            entity.DataAtualizacao = null;
        }

        public virtual Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(SetUpdateData(entity));
            return Task.CompletedTask;
        }

        private T SetUpdateData(T entity)
        {
            entity.DataAtualizacao = DateTime.Now;
            return entity;
        }

        public virtual Task UpdateCollectionAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(SetUpdateData(entities));
            return Task.CompletedTask;
        }

        private IEnumerable<T> SetUpdateData(IEnumerable<T> entities)
        {
            return entities.Select(entity => { return SetUpdateData(entity); });
        }
    }
}
