using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Service.Services;
using MS.CoachSystem.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MS.CoachSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var tokenResponse = await _authService.LoginAsync(loginDto);

            if (tokenResponse.IsSuccessfull)
            {
                // Token'ı saklayın ve yönlendirme yapın
                HttpContext.Response.Cookies.Append("AccessToken", tokenResponse.Data.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                }); 
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Login failed");
            return View(loginDto);

        }

        [Authorize(Roles = "coach")]
        public IActionResult Test()
        {
            return View();
        }
    }
}
