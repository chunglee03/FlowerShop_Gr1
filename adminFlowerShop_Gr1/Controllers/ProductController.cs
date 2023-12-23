using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace adminFlowerShop_Gr1.Controllers
{
    public class ProductController : Controller
    {
        private readonly FlowerShop_Group1Context _context;

        public ProductController(FlowerShop_Group1Context context)
        {
            _context = context;
        }

        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var IsPages = _context.TblProducts
                    .AsNoTracking()
                    .OrderByDescending(x => x.DateCreated);
                PagedList<TblProduct> models = new PagedList<TblProduct>(IsPages, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //[Route("/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page=1)
        {
            try
            {
                var pageSize = 10;
                var danhmuc = _context.TblCategories.AsNoTracking().SingleOrDefault(x=>x.Alias == Alias);
                var IsPages = _context.TblProducts
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<TblProduct> models = new PagedList<TblProduct>(IsPages, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("/{Alias}-{id}.html", Name="ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.TblProducts.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.TblProducts.AsNoTracking()
                    .Where(x=>x.CatId==product.CatId && x.ProductId != id && x.Active==true)
                    .OrderByDescending(x=>x.DateCreated)
                    .Take(6)
                    .ToList();

                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
