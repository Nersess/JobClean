using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enums
{
    public enum EmploymentType
    {
        None=0,
        [Description("Full Time")]
        FullTime =1,
        [Description("Part Time")]
        PartTime,
        [Description("Contractor")]
        Contractor,
        [Description("Intern")]
        Intern,
        [Description("Seasonal / Temp")]
        SeasonalTemp
    }
}
