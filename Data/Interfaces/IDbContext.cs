using Core.Caching.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> SetEntity<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Save async
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));        

    }
}
