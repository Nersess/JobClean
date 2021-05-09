using AutoMapper;
using Core.Domains.Vacancys;
using Models.VacancyCategory;

namespace JobsClean.Web.ModelDomainMapping
{
    public class VacancyCategoryProfile : Profile
    {
        public VacancyCategoryProfile()
        {
            CreateMap<VacancyCategoryModel, VacancyCategory>().ReverseMap();            
        }
    }
}
