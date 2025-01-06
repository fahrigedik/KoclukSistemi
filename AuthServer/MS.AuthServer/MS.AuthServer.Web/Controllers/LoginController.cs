using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MS.AuthServer.Core.Entities;
using MS.AuthServer.Core.Enum;
using MS.AuthServer.Web.Models;
using MS.AuthServer.Web.ViewModels;

namespace MS.AuthServer.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          //  var appUser = new AppUser()
          //  {
          //      BirthDate = DateTime.UtcNow,
          //      Email = "fahrican9978@gmail.com",
          //      Gender = Gender.Male,
          //      Name = "Fahri",
          //      Surname = "Gedik",
          //      UserName = "FahriGediks159",
          //      UserDescription = "Fahri Gedik, sistem yöneticisidir.",
          //      Id = Guid.NewGuid().ToString()
          //  };
          //var result = await _userManager.CreateAsync(appUser, "Farcanimos9978.");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre hatalı");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, 
                model.RememberMe, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Hesabınız kilitlendi, lütfen daha sonra tekrar deneyin");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Email veya şifre hatalı");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}