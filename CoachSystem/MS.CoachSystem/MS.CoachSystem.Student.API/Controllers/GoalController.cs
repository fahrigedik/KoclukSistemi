﻿using System.Net;
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
        private readonly ICoachStudentService _coachStudentService;

        public GoalController(IGoalService goalService, ICoachStudentService coachStudentService)
        {
            _goalService = goalService;
            _coachStudentService = coachStudentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoals([FromQuery] string studentId)
        {
            var coachId = await _coachStudentService.GetCoachIdByStudentIdAsync(studentId);

            if (coachId.Data is null)
            {
                return await ActionResultInstanceAsync(coachId);
            }

            var request = new GoalRequestDto { StudentID = studentId, CoachID = coachId.Data.FirstOrDefault() };

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

            var completedGoalResponse = await _goalService.MarkGoalAsCompletedAsync(goal);

            var updatedGoal = await _goalService.UpdateAsync(completedGoalResponse.Data, id);
            return await ActionResultInstanceAsync(GenericResponse<GoalDto>.Success(updatedGoal, HttpStatusCode.OK));
        }


        [HttpPatch("{id}/working")]
        public async Task<IActionResult> MarkGoalAsWorking(int id)
        {
            var goal = await _goalService.GetByIdAsync(id);
            if (goal == null)
            {
                return ActionResultInstance(GenericResponse<string>.Fail("Goal is not found", HttpStatusCode.OK, true));
            }

            var completedGoalResponse = await _goalService.MarkGoalAsWorkingAsync(goal);

            var updatedGoal = await _goalService.UpdateAsync(completedGoalResponse.Data, id);
            return await ActionResultInstanceAsync(GenericResponse<GoalDto>.Success(updatedGoal, HttpStatusCode.OK));
        }
    }
}
