using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    public class MemoryRepository<T, TKey> : IRepository<T, TKey> where T : class
    {
        private Dictionary<TKey, T> _store = new Dictionary<TKey, T>();

        /// <inheritDoc/>
        public void Insert(TKey id, T entity)
        {
            if (_store.ContainsKey(id))
                throw new ArgumentException("The given ID already exists in the repository", "id");

            _store.Add(id, entity);
        }

        /// <inheritDoc/>
        public void Update(TKey id, T entity)
        {
            if (!_store.ContainsKey(id))
                throw new ArgumentException("The given ID does not exist in the repository", "id");

            _store[id] = entity;
        }

        /// <inheritDoc/>
        public T DeleteById(TKey id)
        {
            if (!_store.ContainsKey(id))
                throw new ArgumentException("The given ID does not exist in the repository", "id");

            T entity = _store[id];
            _store.Remove(id);

            return entity;
        }

        /// <inheritDoc/>
        public IQueryable<T> GetAll()
        {
            return _store.Values.AsQueryable();
        }

        /// <inheritDoc/>
        public T GetById(TKey id)
        {
            if (!_store.ContainsKey(id))
                return null;

            return _store[id];
        }
    }
}
