using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MovieProject.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                string logText = $"{context.Request?.Method} {context.Request?.Path.Value} => {context.Response?.StatusCode}{Environment.NewLine}";
                File.AppendAllText("ControllerLogging.txt", logText);
            }
        }
    }
}
