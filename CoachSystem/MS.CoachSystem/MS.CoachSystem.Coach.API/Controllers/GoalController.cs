using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MS.CoachSystem.Coach.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {



        [Authorize(Roles="coach")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test");
        }
    }
}
