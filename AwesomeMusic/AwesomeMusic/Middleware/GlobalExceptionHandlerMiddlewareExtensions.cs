namespace AwesomeMusic.Middleware
{
    using System.Linq;
    using System.Net;
    using AwesomeMusic.Data.DTOs;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;

                    if (contextFeature != null)
                    {
                        Response<string> response = new()
                        {
                            Message = "An error has occurred",
                            Error = $"{contextFeature.Error.Message} {contextFeature.Error.InnerException} {contextFeature.Error.StackTrace}"
                        };

                        if (exception.GetType().Equals(typeof(ValidationException)))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            response.Error = ((ValidationException)exception).Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                            response.Message = "One or more validation errors occurred.";
                        }

                        var err = JsonConvert.SerializeObject(response);

                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
            });
        }
    }
}
