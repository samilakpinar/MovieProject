using Microsoft.AspNetCore.Http;
using MovieProject.Models;
using NLog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MovieProject.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var now = DateTime.UtcNow;

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Exception error: " + $"{now.ToString("HH:mm:ss")} : {ex}");

            return httpContext.Response.WriteAsync(new ErrorResultModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
    }
}
