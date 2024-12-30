using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
using MS.CoachSystem.Core.Services;

namespace MS.CoachSystem.Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachingResourceController : CustomBaseController
    {
        private readonly ICoachingResourceService _coachingResourceService;

        public CoachingResourceController(ICoachingResourceService coachingResourceService)
        {
            _coachingResourceService = coachingResourceService;
        }


        [Authorize(Roles = "student")]
        [HttpGet]
        public async Task<IActionResult> GetCoachingResourcesByStudentId([FromQuery] CoachingResourceRequestDto requestDto)
        {
            var resources = await _coachingResourceService.GetAllCoachingResourceWithDetailByStudentIdAsync(requestDto);
            return await ActionResultInstanceAsync(resources);
        }
    }
}
