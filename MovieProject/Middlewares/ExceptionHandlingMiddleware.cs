using Microsoft.AspNetCore.Http;
using MovieProject.Result;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MovieProject.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            var errorMessage = JsonConvert.SerializeObject(
                ServiceResult<object>.CreateError(HttpStatusCode.InternalServerError, ex.Message));


            return context.Response.WriteAsync(errorMessage);
        }
    }
}
