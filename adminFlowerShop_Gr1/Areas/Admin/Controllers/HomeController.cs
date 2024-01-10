using adminFlowerShop_Gr1.Helpper;
using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Logout()
        {
            Utilities._UserID = 0;
            Utilities._UserName = string.Empty;
            Utilities._Email = string.Empty;
            Utilities._Message = string.Empty;
            Utilities._MessageEmail = string.Empty;

            return RedirectToAction("Index", "Login");
        }
        public IActionResult Index()
        {
            if (!Utilities.IsLogin())
                return RedirectToAction("Index", "Login");
            return View();
        }
    }
}
