using Core.Enums;
using Models.Common;
using Models.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Vacancy
{
    public class VacancySearchModel : RequestModel
    {
        public string Title { get; set; }
        public EmploymentType Type { get; set; }
        public int[] Categories { get; set; }
        public IList<int> LocationIds { get; set; }
    }
}
