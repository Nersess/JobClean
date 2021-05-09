using Core.Caching.Domains;
using Core.Domains.Common;
using Core.Domains.Location;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains.Vacancys
{
    public class Vacancy : BaseEntity
    {
        public Vacancy() { }

        /// <summary>
        /// Gets or sets user Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets short description
        /// </summary>
        public string ShoetDescription { get; set; }
        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets Responsibilities
        /// </summary>
        public string Responsibilities { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets EmploymentType
        /// </summary>
        public EmploymentType EmploymentType { get; set; }

        /// <summary>
        /// Gets or sets location
        /// </summary>
        public City Location { get; set; }

        /// <summary>
        /// Gets or sets category of Vacancy
        /// </summary>
        public virtual IList<VacancyCategoryRef> VacancyCategoryRef { get; set; }

        /// <summary>
        /// Many to many BookmarkVacancy
        /// </summary>
        public IList<BookmarkVacancy> BookmarkVacancys { get; set; }



    }
}
