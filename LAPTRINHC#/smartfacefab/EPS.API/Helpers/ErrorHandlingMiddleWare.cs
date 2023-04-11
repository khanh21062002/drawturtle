using EPS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EPS.API.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                //var lang = context.Request.Headers["Accept-Language"].ToString();

                //if (lang == EPS.Common.Helper.GetEnumDescription(SupportedLanguage.Japanese))
                //{
                //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ja-JP");
                //}
                //else if (lang == EPS.Common.Helper.GetEnumDescription(SupportedLanguage.Vietnamese))
                //{
                //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
                //}
                //else
                //{
                //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                //}

                            await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is MyException) code = HttpStatusCode.BadRequest;
            exception = exception.GetBaseException();
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            logger.LogError(exception, "Unexpected error occurred. {errMsg}", exception.Message);

            return context.Response.WriteAsync(result);
        }
    }
}
