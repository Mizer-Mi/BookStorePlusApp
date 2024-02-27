using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;

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
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _=> StatusCodes.Status500InternalServerError
                        };
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails() {StatusCode=context.Response.StatusCode,  Message= contextFeature.Error.Message}.ToString());
                    }
                });
            });
        }
    }
}
