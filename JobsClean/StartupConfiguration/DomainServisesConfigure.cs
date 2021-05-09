using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsClean.Web.StartupConfiguration
{
    public static class DomainServisesConfigure
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVacancyServices, VacancyServices>();
        }
    }
}
