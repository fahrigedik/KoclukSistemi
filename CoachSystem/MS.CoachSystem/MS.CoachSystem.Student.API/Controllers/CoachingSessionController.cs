using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs.CoachingSessionDtos;
using MS.CoachSystem.Core.Services;

namespace MS.CoachSystem.Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachingSessionController : CustomBaseController
    {
        private readonly ICoachingSessionService _coachingSessionService;

        public CoachingSessionController(ICoachingSessionService coachingSessionService)
        {
            _coachingSessionService = coachingSessionService;
        }

        [Authorize(Roles = "student")]
        [HttpGet]
        public async Task<IActionResult> GetCoachingSessions([FromQuery] CoachingSessionRequestDto requestDto)
        {
            var sessions = await _coachingSessionService.GetCoachingSessionByStudentIdAsync(requestDto);
            return await ActionResultInstanceAsync(sessions);
        }
    }
}
