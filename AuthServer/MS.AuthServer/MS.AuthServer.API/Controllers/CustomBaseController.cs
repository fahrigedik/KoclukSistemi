using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(GenericResponse<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode.GetHashCode()
            };
        }   

        public async Task<IActionResult> ActionResultInstanceAsync<T>(Task<GenericResponse<T>> response) where T : class
        {
            var result = await response;
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode.GetHashCode()
            };
        }
    }
}
