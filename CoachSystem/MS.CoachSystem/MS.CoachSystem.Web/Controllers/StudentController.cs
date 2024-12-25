using System.Security.Claims;
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
        private readonly StudentService _studentService;
        private readonly CoachStudentService _coachStudentService;
        public StudentController(
            AuthService authService, 
            StudentService studentService,
            CoachStudentService coachStudentService)
        {
            _authService = authService;
            _studentService = studentService;
            _coachStudentService = coachStudentService;
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
            var result = await _studentService.CreateStudentUserAsync(new CreateUserDto()
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
                _coachStudentService.AddAsync(new CoachStudentDto()
                {
                    CoachId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    StudentId = result.Data.Id
                });
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Kullanıcı oluşturulamadı.");
            return View(createStudentViewModel);
        }
    }
}
