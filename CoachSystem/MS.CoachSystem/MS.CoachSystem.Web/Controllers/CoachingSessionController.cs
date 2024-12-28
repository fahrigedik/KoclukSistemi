using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.Services;
using System.Security.Claims;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.CoachingSessionDtos;
using MS.CoachSystem.Entity.Entities;
using MS.CoachSystem.Web.ViewModels;

namespace MS.CoachSystem.Web.Controllers
{
    public class CoachingSessionController : Controller
    {

        private readonly ICoachingSessionService _coachingSessionService;

        public CoachingSessionController(ICoachingSessionService coachingSessionService)
        {
            _coachingSessionService = coachingSessionService;
        }
        public async Task<IActionResult> Index()
        {
            var coachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = HttpContext.Session.GetString("ManagedStudentId");

            var coachingSessionRequestDto = new CoachingSessionRequestDto()
            {
                CoachID = coachId,
                StudentID = studentId
            };

            var coachingSessions =
                await _coachingSessionService.GetCoachingSessionByStudentIdAsync(coachingSessionRequestDto);
            return View(coachingSessions.Data);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoachingSessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var session = new CoachingSessionDto()
                {
                    CoachId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    StudentId = HttpContext.Session.GetString("ManagedStudentId"),
                    SessionTopic = model.SessionTopic,
                    SessionNotes = model.SessionNotes,
                    SessionLocation = model.SessionLocation,
                    SessionDate = model.SessionDate,
                    SessionTime = model.SessionTime,
                    SessionStatus = model.SessionStatus
                };

                await _coachingSessionService.AddAsync(session);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var remove = await _coachingSessionService.DeleteAsync(id);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var session = await _coachingSessionService.GetMainEntityByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            var model = new UpdateCoachingSessionViewModel
            {
                Id = session.Id,
                SessionTopic = session.SessionTopic,
                SessionNotes = session.SessionNotes,
                SessionLocation = session.SessionLocation,
                SessionDate = session.SessionDate,
                SessionTime = session.SessionTime,
                SessionStatus = session.SessionStatus
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCoachingSessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var session = new CoachingSessionDto()
                {
                    CoachId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    StudentId = HttpContext.Session.GetString("ManagedStudentId"),
                    SessionTopic = model.SessionTopic,
                    SessionNotes = model.SessionNotes,
                    SessionLocation = model.SessionLocation,
                    SessionDate = model.SessionDate,
                    SessionTime = model.SessionTime,
                    SessionStatus = model.SessionStatus
                };

                await _coachingSessionService.UpdateAsync(session, model.Id);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
