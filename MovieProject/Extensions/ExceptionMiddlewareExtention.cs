using Microsoft.AspNetCore.Builder;
using MovieProject.Middlewares;

namespace MovieProject.Extensions
{
    public static class ExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
