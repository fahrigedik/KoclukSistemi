using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.GoalDtos;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Entity.Entities;
using MS.CoachSystem.Web.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace MS.CoachSystem.Web.Controllers
{
    [Authorize(Roles = "coach")]
    public class GoalController : Controller
    {
        private readonly IGoalService _goalService;
        private readonly IGoalTypeService _goalTypeService;

        public GoalController(IGoalService goalService, IGoalTypeService goalTypeService)
        {
            _goalService = goalService;
            _goalTypeService = goalTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var coachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = HttpContext.Session.GetString("ManagedStudentId");
            var requestDto = new GoalRequestDto() { CoachID = coachId, StudentID = studentId };

            var goals = await _goalService.GetAllGoalWithTypeByStudentId(requestDto);
            return View(goals.Data);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var goalTypes = await _goalTypeService.GetAllMainEntitiesAsync();
            var goal = await _goalService.GetMainEntityByIdAsync(id);
            var goalTypeSelectList = goalTypes.Select(gt => new SelectListItem
            {
                Value = gt.Id.ToString(),
                Text = gt.TypeName
            }).ToList();
            var updateGoalViewModel = new UpdateGoalViewModel
            {
                Goal = goal,
                GoalTypes = goalTypeSelectList
            };
            return View(updateGoalViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateGoalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var goalDto = new GoalDto
                {
                    CoachID = model.Goal.CoachID,
                    StudentID = model.Goal.StudentID,
                    GoalTitle = model.Goal.GoalTitle,
                    GoalDescription = model.Goal.GoalDescription,
                    GoalTypeId = model.Goal.GoalTypeId,
                    DueDate = model.Goal.DueDate,
                    CompletedDate = model.Goal.CompletedDate,
                    Status = model.Goal.Status,
                    ModificationDate = DateTime.Now
                };

                await _goalService.UpdateAsync(goalDto, model.Goal.Id);
                
                return RedirectToAction("Index", "Goal", new GoalRequestDto(){CoachID = model.Goal.CoachID, StudentID = model.Goal.StudentID});
            }

            var goalTypes = await _goalTypeService.GetAllMainEntitiesAsync();
            model.GoalTypes = goalTypes.Select(gt => new SelectListItem
            {
                Value = gt.Id.ToString(),
                Text = gt.TypeName
            }).ToList();

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _goalService.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var goalTypes = await _goalTypeService.GetAllMainEntitiesAsync();
            var goalTypeSelectList = goalTypes.Select(gt => new SelectListItem
            {
                Value = gt.Id.ToString(),
                Text = gt.TypeName
            }).ToList();

            var createGoalViewModel = new CreateGoalViewModel
            {
                GoalTypes = goalTypeSelectList
            };

            return View(createGoalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGoalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var goalDto = new GoalDto
                {
                    CoachID = model.CoachID,
                    StudentID = model.StudentID,
                    GoalTitle = model.GoalTitle,
                    GoalDescription = model.GoalDescription,
                    GoalTypeId = model.GoalTypeId,
                    DueDate = model.DueDate,
                    CompletedDate = model.CompletedDate,
                    Status = model.Status,
                    CreationDate = DateTime.Now
                };

                await _goalService.AddAsync(goalDto);

                return RedirectToAction("Index", "Goal", new GoalRequestDto { CoachID = model.CoachID, StudentID = model.StudentID });
            }

            var goalTypes = await _goalTypeService.GetAllMainEntitiesAsync();
            model.GoalTypes = goalTypes.Select(gt => new SelectListItem
            {
                Value = gt.Id.ToString(),
                Text = gt.TypeName
            }).ToList();

            return View(model);
        }
    }
}
