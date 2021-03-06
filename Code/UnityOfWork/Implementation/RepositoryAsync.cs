﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LogbookApi.UnityOfWork.Implementation
{
    internal sealed class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly DbContext _context;

        public RepositoryAsync(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        public IQueryable<T> All => _context.Set<T>();

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(All, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}