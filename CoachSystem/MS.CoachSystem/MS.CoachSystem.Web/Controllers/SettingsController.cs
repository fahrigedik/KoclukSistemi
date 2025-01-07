using Microsoft.AspNetCore.Mvc;

namespace MS.CoachSystem.Web.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
