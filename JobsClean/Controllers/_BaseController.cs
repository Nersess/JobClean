using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsClean.Web.Controllers
{    
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class _BaseController : Controller
    {
        protected readonly ILogger<_BaseController> Logger;

        public _BaseController(ILogger<_BaseController> logger)
        {
            this.Logger = logger;
        }

        protected IActionResult HandleExeption(Exception ex)
        {
            this.Logger.LogError( ex, "HandleExeption from BaseController");            

            return this.StatusCode(StatusCodes.Status400BadRequest, ex.GetBaseException().Message);
        }
    }
}
