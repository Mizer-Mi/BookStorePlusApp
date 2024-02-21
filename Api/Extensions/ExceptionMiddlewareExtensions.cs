using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using Entities.ErrorModel;

namespace Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler (this WebApplication app,ILoggerService logger)
        {
            app.UseExceptionHandler(appErr =>
            {
                appErr.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails() {StatusCode=context.Response.StatusCode,  Message= "Internal Server Error"}.ToString());
                    }
                });
            });
        }
    }
}
