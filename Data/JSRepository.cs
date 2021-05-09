using Core.Caching.Domains;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Repository for Vacancy Search 
    /// </summary>
    public class JSRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private DbSet<T> _entities;

        public JSRepository(IDbContext context)
        {
            this._context = context;
        }

        #region Properties
        public virtual IQueryable<T> Table => this.Entities;        
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (this._entities == null)
                {
                    this._entities = this._context.SetEntity<T>();
                }

                return this._entities;
            }
        }
        #endregion

        #region Async Methods
        public ValueTask<T> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return this.Entities.FindAsync(id);
        }

        public Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Add(entity);

            return this.SaveChangesAsync(cancellationToken);
        }

        public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                this.Entities.Add(entity);
            }

            return this.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Update(entity);

            return this.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            this.Entities.UpdateRange(entities);

            return this.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int entityId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entityId <= 0)
            {
                throw new ArgumentException(nameof(entityId));
            }

            var entity = await this.GetByIdAsync(entityId);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Remove(entity);

            this._context.SaveChanges();
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Remove(entity);

            return this.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                this.Entities.Remove(entity);
            }

            return this.SaveChangesAsync(cancellationToken);
        }       
        private Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this._context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
