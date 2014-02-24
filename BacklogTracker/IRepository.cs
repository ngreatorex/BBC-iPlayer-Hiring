using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    /// <summary>
    /// A generic repository pattern for storage of entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to be stored</typeparam>
    /// <typeparam name="TKey">The type of the primary key for the entity</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        /// <summary>
        /// Store a new entity in the repository
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the key already exists in the repository</exception>
        /// <param name="id">The key for the new entity</param>
        /// <param name="entity">The entity to store</param>
        void Insert(TKey id, TEntity entity);

        /// <summary>
        /// Update an existing entity in the repository
        /// </summary>
        /// <param name="id">The key of the entity that should be updated</param>
        /// <param name="entity">The new entity to store against the existing key</param>
        void Update(TKey id, TEntity entity);

        /// <summary>
        /// Delete an existing entity from the repository
        /// </summary>
        /// <param name="id">The key of the entity to delete</param>
        /// <returns>The entity as it was just before deletion</returns>
        TEntity DeleteById(TKey id);

        /// <summary>
        /// Return all the entities in the repository
        /// </summary>
        /// <returns>An IQueryable of all the entities</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get one specific entity from the repository
        /// </summary>
        /// <param name="id">The key of the entity to return</param>
        /// <returns>The entity that matches the given key</returns>
        TEntity GetById(TKey id);
    }
}
