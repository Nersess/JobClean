using Core.Caching.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains.Location
{
    public class City : BaseEntity
    {
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Latitude of vacancy coordinates 
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// Longitude of vacancy coordinates 
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// State of the city
        /// </summary>
        public int CurrentStateId { get; set; }
        public State State { get; set; }
    }
}
