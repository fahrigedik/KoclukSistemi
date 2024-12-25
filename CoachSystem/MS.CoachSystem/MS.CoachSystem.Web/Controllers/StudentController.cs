using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Service.Services;
using MS.CoachSystem.Web.ViewModels;

namespace MS.CoachSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly AuthService _authService;
        public StudentController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "coach")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize(Roles = "coach")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel createStudentViewModel)
        {
            var result = await _authService.CreateStudentUserAsync(new CreateUserDto()
            {
                BirthDate = createStudentViewModel.BirthDate,
                City = createStudentViewModel.City,
                Email = createStudentViewModel.Email,
                Gender = createStudentViewModel.Gender,
                Name = createStudentViewModel.Name,
                Password = createStudentViewModel.Password,
                Surname = createStudentViewModel.Surname,
                TCKN = createStudentViewModel.TCKN,
                UserName = createStudentViewModel.UserName
            });

            if (result.IsSuccessfull)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Kullanıcı oluşturulamadı.");
            return View(createStudentViewModel);
        }
    }
}
