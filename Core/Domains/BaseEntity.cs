using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Caching.Domains
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public virtual int Id { get; set; }
    }
}
