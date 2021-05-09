using Core.Enums;
using Models.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Vacancy
{
    public class VacancyModel
    {
        public string Title { get; set; }        
        public string ShoetDescription { get; set; } 
      
    }

    public class VacancyDitailModel : VacancyModel
    {
        public string Description { get; set; }

        public string Responsibilities { get; set; }
        public DateTime CreatedOn { get; set; }

        public EmploymentType EmploymentType { get; set; }
        public LocationModel Location { get; set; }
    }
}
