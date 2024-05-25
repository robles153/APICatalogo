using APICatalogo.Domain;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace APICatalogo.Extensions;

public static class ApiEceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var error = contextFeature.Error;
                    string message = error.Message;
                    string stackTrace = error.StackTrace;

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = message,
                        Trace = stackTrace
                    }.ToString());
                }
            });
        });
    }

}
