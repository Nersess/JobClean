using Core.Caching.Domains;
using Core.Domains.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains.Vacancys
{
    public class VacancyCategoryRef : BaseEntity
    {
        public int VacancyCategoryId { get; set; }        
        public VacancyCategory VacancyCategory { get; set; }
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }
    }
}
