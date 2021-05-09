using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Vacancy;
using Models.Common;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domains.Vacancys;
using Core.Paging;
using Microsoft.AspNetCore.Http;

namespace JobsClean.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VacancyController : _BaseController
    {
        private readonly IVacancyServices _jobService;
        private readonly IMapper _mapper;

        public VacancyController(IMapper mapper, IVacancyServices jobService, ILogger<AcountController> logger) : base(logger)
        {
            this._mapper = mapper;
            this._jobService = jobService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<VacancyModel>))]
        public async Task<IActionResult> Search([FromBody] VacancySearchModel model)
        {
            var vacancies = await _jobService.GetAllVacancyAsync(model.Categories, model.LocationIds, model.Type, model.Skip, model.Take);

            if (vacancies == null || vacancies.Count == 0)
            {
                return NotFound();
            }

            var vacncyModel = _mapper.Map<List<Vacancy>, ResponseModel<VacancyModel>>(vacancies);

            return Ok(vacncyModel);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyDitailModel))]
        [ResponseCache(Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetVacancy([FromQuery] int id)
        {
            var vacancy = await _jobService.GetVacancyByIdAsync(id);

            if (vacancy == null)
            {
                return NotFound();
            }

            var vacncyModel = _mapper.Map<Vacancy, VacancyDitailModel>(vacancy);

            return Ok(vacncyModel);
        }
    }
}
