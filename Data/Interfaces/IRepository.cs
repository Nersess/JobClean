using Core.Caching.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    public interface IRepository<T> where T : BaseEntity
    {
        #region Async Methods
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Entity</returns>
        ValueTask<T> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Add entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken"></param>
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity by Id async
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(int entityId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));
        
        #endregion
       
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }
      
    }
}
