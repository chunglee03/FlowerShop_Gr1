using adminFlowerShop_Gr1.Models;
using adminFlowerShop_Gr1.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace adminFlowerShop_Gr1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FlowerShop_Group1Context _context;

        public HomeController(ILogger<HomeController> logger,FlowerShop_Group1Context context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            HomeViewVM model = new HomeViewVM();
            var IsProducts = _context.TblProducts.AsNoTracking().Where(x => x.Active == true && x.HomeFlag==true).OrderByDescending(x => x.DateCreated).ToList();
            var blog = _context.TblPosts.AsNoTracking().Where(x => x.Published == true && x.IsNewFeed == true).OrderByDescending(x => x.CreatedDate).Take(3).ToList();
            //model.Products= IsProducts;
            model.Blog = blog;
            ViewBag.AllProduct = IsProducts;
            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("gioi-thieu.html", Name = "About")]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}