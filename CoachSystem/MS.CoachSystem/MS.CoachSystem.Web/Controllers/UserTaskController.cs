using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Core.Services;
using System.Security.Claims;
using MS.CoachSystem.Entity.Entities;

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


        public async Task<IActionResult> Remove(int id)
        {
            var remove = await _userTaskService.DeleteAsync(id);
            return RedirectToAction("Index", "UserTask");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var userTask = await _userTaskService.GetMainEntityByIdAsync(id);
            return View(userTask);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserTask userTask)
        {
            var result = await _userTaskService.UpdateAsync(new UserTaskDto()
            {
                CoachID = userTask.CoachID,
                StudentID = userTask.StudentID,
                CompletedDate = userTask.CompletedDate,
                Description = userTask.Description,
                CreationDate = userTask.CreationDate,
                DueDate = userTask.DueDate,
                ModificationDate = userTask.ModificationDate,
                Priority = userTask.Priority,
                Status = userTask.Status,
                Title = userTask.Title
            }, userTask.Id);

            return RedirectToAction("Index", "UserTask");
        }
    }
}
