using AutoMapper;
using Core.Domains.Users;
using Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsClean.Web.ModelDomainMapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginModel, User>().ReverseMap();
            CreateMap<RegisterModel, User>().ReverseMap();
        }
    }
}
