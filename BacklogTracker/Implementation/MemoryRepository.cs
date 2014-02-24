﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    public class MemoryRepository<T, TKey> : IRepository<T, TKey> where T : class
    {
        private Dictionary<TKey, T> _store = new Dictionary<TKey, T>();

        /// <summary>
        /// Insert a new entry into the repository
        /// </summary>
        /// <param name="id">The primary key of the new entity</param>
        /// <param name="entity">The entity to insert</param>
        /// <exception cref="System.ArgumentException">The given ID already exists in the repository. Try <see cref="Update"/> instead.</exception>
        public void Insert(TKey id, T entity)
        {
            if (_store.ContainsKey(id))
                throw new ArgumentException("The given ID already exists in the repository", "id");

            _store.Add(id, entity);
        }

        public void Update(TKey id, T entity)
        {
            if (!_store.ContainsKey(id))
                throw new ArgumentException("The given ID does not exist in the repository", "id");

            _store[id] = entity;
        }

        public T DeleteById(TKey id)
        {
            if (!_store.ContainsKey(id))
                throw new ArgumentException("The given ID does not exist in the repository", "id");

            T entity = _store[id];
            _store.Remove(id);

            return entity;
        }

        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _store.Values.AsQueryable().Provider.CreateQuery<T>(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _store.Values.AsQueryable();
        }

        public T GetById(TKey id)
        {
            if (!_store.ContainsKey(id))
                return null;

            return _store[id];
        }
    }
}
