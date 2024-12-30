using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;

namespace MS.CoachSystem.Student.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(GenericResponse<T> response) where T : class
        {
            return new ObjectResult(response)
            {
            };
        }

        public async Task<IActionResult> ActionResultInstanceAsync<T>(GenericResponse<T> response) where T : class
        {
            return new ObjectResult(response)
            {
            };
        }

    }
}
