using AutoMapper;
using Core.Domains.Vacancys;
using Models.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsClean.Web.ModelDomainMapping
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<VacancyModel, Vacancy>().ReverseMap();
            CreateMap<VacancyDitailModel, Vacancy>().ReverseMap();
        }
    }
}
