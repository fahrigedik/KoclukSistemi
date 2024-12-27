using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Service.Services;
using MS.CoachSystem.Web.ViewModels;

namespace MS.CoachSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly AuthService _authService;
        private readonly StudentService _studentService;
        private readonly ICoachStudentService _coachStudentService;
        public StudentController(
            AuthService authService,
            StudentService studentService,
            ICoachStudentService coachStudentService)
        {
            _authService = authService;
            _studentService = studentService;
            _coachStudentService = coachStudentService;
        }

        public async Task<IActionResult> Index()
        {
            var studentIds = await _coachStudentService.GetStudentIdsByCoachIdAsync(HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var students = await _studentService.GetStudentsByIdsAsync(studentIds.Data);

            return View(students.Data);
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

            if (result.Error is null)
            {
                await _coachStudentService.AddAsync(new CoachStudentDto()
                {
                    CoachId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    StudentId = result.Data.Id
                });
            }

            return RedirectToAction("Index", "Home");


            ModelState.AddModelError(string.Empty, "Kullanıcı oluşturulamadı.");
            return View(createStudentViewModel);
        }

        [Authorize(Roles = "coach")]
        public async Task<IActionResult> DeleteStudent(string studentId)
        {
            var result = await _studentService.RemoveStudentUserAsync(studentId);
            if (result.Error is null)
            {
                var coachStudentid = await _coachStudentService.GetEntityIdByCoachIdAndStudentId(
                    HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    studentId);

                await _coachStudentService.DeleteAsync(int.Parse(coachStudentid.Data));
            }
            return RedirectToAction("Index", "Student");
        }


        [Authorize(Roles = "coach")]
        public async Task<IActionResult> ManageStudent(string studentId)
        {
            HttpContext.Session.SetString("ManagedStudentId", studentId);
            return View();
        }
    }
}
