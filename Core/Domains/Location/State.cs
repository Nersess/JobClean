using Core.Caching.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains.Location
{
    public class State : BaseEntity
    {
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public Country Country { get; set; }

        public ICollection<City> Citys { get; set; }
    }
}
