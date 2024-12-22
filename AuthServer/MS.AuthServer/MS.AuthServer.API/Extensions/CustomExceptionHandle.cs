using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.API.Extensions;

public static class CustomExceptionHandle
{
    public static void UseCustomExceptionHandle(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                ErrorDto errorDto = null;
                if (errorFeature != null)
                {
                    var ex = errorFeature.Error;
                    errorDto = new ErrorDto(new List<string> { ex.Message }, true);
                }

                var response = GenericResponse<NoDataDto>.Fail(errorDto, HttpStatusCode.InternalServerError);

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });
    }
}