using Microsoft.AspNetCore.Mvc;

namespace adminFlowerShop_Gr1.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
