using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adminFlowerShop_Gr1.Controllers
{
    public class ProductController : Controller
    {
        private readonly FlowerShop_Group1Context _context;

        public ProductController(FlowerShop_Group1Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var product = _context.TblProducts.Include(x=>x.Cat).FirstOrDefault(x => x.ProductId == id);
            if(product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
