using Core.Caching.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains.Vacancys
{
    public class VacancyCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets category of Vacancy
        /// </summary>
        public virtual IList<VacancyCategoryRef> VacancyCategoryRef { get; set; }
    }
}
