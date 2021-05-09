using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JobsClean.Web.ExceptionHandling
{
    public class ApiExceptionMiddleware
    {        
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RequestDelegate _next;

        public ApiExceptionMiddleware(IWebHostEnvironment hostingEnvironment, ILogger<ApiExceptionMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = HttpStatusCode.InternalServerError,
                Title = "Some kind of error occurred in the API.  Please use the id and contact our support team if the problem persists."
            };

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            return context.Response.WriteAsync(result);
        }
    }

    public class ApiError
    {
        public String Id { get; set; }
        public HttpStatusCode Status { get; set; }
        public String Title { get; set; }

    }
}
