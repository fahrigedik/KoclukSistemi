using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Core.Services;
using System.Security.Claims;

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
        public async Task<IActionResult> Index()
        {
            var coachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = HttpContext.Session.GetString("ManagedStudentId");

            var userTaskRequestDto = new UserTaskRequestDto()
            {
                CoachID = coachId,
                StudentID = studentId
            };
            var userTasks = await _userTaskService.GetUserTaskByStudentIdAsync(userTaskRequestDto);
            return View(userTasks.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserTaskDto userTaskDto)
        {
            var result = await _userTaskService.AddAsync(userTaskDto);
            return RedirectToAction("Index", "UserTask");
        }
    }
}
