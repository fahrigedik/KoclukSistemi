using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Core.Services;

namespace MS.CoachSystem.Web.Controllers
{

    [Authorize(Roles = "coach")]
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;

        public UserTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }
        public async Task<IActionResult> Index(UserTaskRequestDto userTaskRequestDto)
        {
            ViewBag.ManagedStudentId = userTaskRequestDto.StudentID;
            var userTasks = await _userTaskService.GetUserTaskByStudentIdAsync(userTaskRequestDto);
            return View(userTasks.Data);
        }
    }
}
