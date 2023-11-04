using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }
    }
}
