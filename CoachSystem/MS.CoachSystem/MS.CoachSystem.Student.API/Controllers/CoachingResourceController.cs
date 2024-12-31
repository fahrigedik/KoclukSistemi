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
        private readonly ICoachStudentService _coachStudentService;

        public CoachingResourceController(ICoachingResourceService coachingResourceService, ICoachStudentService coachStudentService)
        {
            _coachingResourceService = coachingResourceService;
            _coachStudentService = coachStudentService;
        }


        [Authorize(Roles = "student")]
        [HttpGet]
        public async Task<IActionResult> GetCoachingResourcesByStudentId([FromQuery] string studentId)
        {
            var coachId = await _coachStudentService.GetCoachIdByStudentIdAsync(studentId);

            if (coachId.Data is null)
            {
                return await ActionResultInstanceAsync(coachId);
            }

            var requestDto = new CoachingResourceRequestDto()
            {
                CoachID = coachId.Data.First(),
                StudentID = studentId
            };

            var resources = await _coachingResourceService.GetAllCoachingResourceWithDetailByStudentIdAsync(requestDto);
            return await ActionResultInstanceAsync(resources);
                    }
    }
}
