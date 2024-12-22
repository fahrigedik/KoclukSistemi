using System.Net;
using Microsoft.AspNetCore.Mvc;
using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.API.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(e => e.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage).ToList();

                    var errorDto = new ErrorDto(errors, true);
                    var response = GenericResponse<ErrorDto>.Fail(errorDto, HttpStatusCode.BadRequest);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
