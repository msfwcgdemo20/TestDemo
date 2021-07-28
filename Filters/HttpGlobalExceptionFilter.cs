using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestDemo.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            var json = new ErrorModel
            {
                CorrelationId = Guid.NewGuid(),
                ErrorCode = "110",
                Message = "An error occurred. Try it again.",
                Detail = string.Empty,
                HelpUrl = string.Empty
            };


            context.Result = new ContentResult
            {
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(json),
                ContentType = "text/json",
                StatusCode = (int?)HttpStatusCode.InternalServerError
            }; ;
            context.ExceptionHandled = true;
        }
    }

}
