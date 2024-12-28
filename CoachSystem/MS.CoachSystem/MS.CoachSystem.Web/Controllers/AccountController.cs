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
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var tokenResponse = await _authService.LoginAsync(new LoginDto(){Email = loginDto.Email, Password = loginDto.Password});

            if (tokenResponse.IsSuccessfull)
            {
                HttpContext.Response.Cookies.Append("AccessToken", tokenResponse.Data.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                }); 
                return RedirectToAction("Index", "Home");
            }

            tokenResponse.Error.Errors.ToList().ForEach(x => ModelState.AddModelError(string.Empty, x));
            return View(loginDto);
        }
    }
}
