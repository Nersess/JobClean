using Core.Caching.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains.Location
{
    public class Country : BaseEntity
    {
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string Name { get; set; }

        public ICollection<State> States { get; set; }
    }
}
