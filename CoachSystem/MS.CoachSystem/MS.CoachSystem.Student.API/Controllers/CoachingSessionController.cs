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
        private readonly ICoachStudentService _coachStudentService;

        public CoachingSessionController(ICoachingSessionService coachingSessionService, ICoachStudentService coachStudentService)
        {
            _coachingSessionService = coachingSessionService;
            _coachStudentService = coachStudentService;
        }

        [Authorize(Roles = "student")]
        [HttpGet]
        public async Task<IActionResult> GetCoachingSessions([FromQuery] string studentId)
        {
            var coachId = await _coachStudentService.GetCoachIdByStudentIdAsync(studentId);

            if (coachId.Data is null)
            {
                return await ActionResultInstanceAsync(coachId);
            }
            var requestDto = new CoachingSessionRequestDto { StudentID = studentId, CoachID = coachId.Data.FirstOrDefault() };

            var sessions = await _coachingSessionService.GetCoachingSessionByStudentIdAsync(requestDto);
            return await ActionResultInstanceAsync(sessions);
        }
    }
}
