using Core.Caching.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Domains.Users;
using Core.Domains.Vacancys;


namespace Core.Domains.Common
{
    public class BookmarkVacancy : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
