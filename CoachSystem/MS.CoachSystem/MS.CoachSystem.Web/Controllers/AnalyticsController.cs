using Microsoft.AspNetCore.Mvc;

namespace MS.CoachSystem.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
