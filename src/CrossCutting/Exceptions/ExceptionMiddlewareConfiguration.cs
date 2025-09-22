using Caixaverso.Backend.API.Middlewares;

namespace Caixaverso.Backend.CrossCutting.Exceptions
{
    public static class ExceptionMiddlewareConfiguration
    {
        public static void UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
