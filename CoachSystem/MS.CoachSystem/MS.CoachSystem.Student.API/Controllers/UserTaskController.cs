using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Core.Services;

namespace MS.CoachSystem.Student.API.Controllers
{
    [Authorize(Roles = "student")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : CustomBaseController
    {
        private readonly IUserTaskService _userTaskService;

        public UserTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTasks([FromQuery] UserTaskRequestDto request)
        {
            var tasks = await _userTaskService.GetUserTaskByStudentIdAsync(request);
            return await ActionResultInstanceAsync(tasks);
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkTaskAsCompleted(int id)
        {
            var task = await _userTaskService.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.CompletedDate = DateTime.Now;
            var updatedTask = await _userTaskService.UpdateAsync(task, id);
            return await ActionResultInstanceAsync(GenericResponse<UserTaskDto>.Success(updatedTask, HttpStatusCode.OK));
        }
    }
}
