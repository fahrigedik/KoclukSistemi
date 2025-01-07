using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Web.Controllers
{
    public class GoalTypeController : Controller
    {
        private readonly IGoalTypeService _goalTypeService;

        public GoalTypeController(IGoalTypeService goalTypeService)
        {
            _goalTypeService = goalTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var goalTypes = await _goalTypeService.GetAllMainEntitiesAsync();
            return View(goalTypes);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var goalType = await _goalTypeService.GetByIdAsync(id);
            var result = await _goalTypeService.DeleteAsync(id);
            return RedirectToAction("Index", "GoalType");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GoalTypeDto goalTypeDto)
        {

            var result = await _goalTypeService.AddAsync(goalTypeDto);

            return RedirectToAction("Index", "GoalType");
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _goalTypeService.GetMainEntityByIdAsync(id);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GoalType goalType)
        {
            if (!ModelState.IsValid)
                return View(goalType);

            try
            {
                var goalTypeDto = new GoalTypeDto()
                {
                    Description = goalType.Description,
                    TypeName = goalType.TypeName
                };

                var result = await _goalTypeService.UpdateAsync(goalTypeDto, goalType.Id);
                TempData["Success"] = "Hedef tipi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Güncelleme sırasında bir hata oluştu.");
                return View(goalType);
            }
        }
    }
}
