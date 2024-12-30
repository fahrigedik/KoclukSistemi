using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.GoalDtos;
using MS.CoachSystem.Core.Services;

namespace MS.CoachSystem.Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : CustomBaseController
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoals([FromQuery] GoalRequestDto request)
        {
            var goals = await _goalService.GetAllGoalWithTypeByStudentId(request);
            return await ActionResultInstanceAsync(goals);
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkGoalAsCompleted(int id)
        {
            var goal = await _goalService.GetByIdAsync(id);
            if (goal == null)
            {
                return ActionResultInstance(GenericResponse<string>.Fail("Goal is not found", HttpStatusCode.OK, true));
            }

            goal.CompletedDate = DateTime.Now;
            var updatedGoal = await _goalService.UpdateAsync(goal, id);
            return await ActionResultInstanceAsync(GenericResponse<GoalDto>.Success(updatedGoal, HttpStatusCode.OK));
        }
    }
}
