using adminFlowerShop_Gr1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace adminFlowerShop_Gr1.Controllers
{
    public class BlogController : Controller
    {
        private readonly FlowerShop_Group1Context _context;

        public BlogController(FlowerShop_Group1Context context)
        {
            _context = context;
        }

        [Route("blog.html", Name = "Blog")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var IsPages = _context.TblPosts
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<TblPost> models = new PagedList<TblPost>(IsPages, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }

        [Route("/bai-viet/{Alias}-{id}.html", Name = "BaiVietDetails")]
        public IActionResult Details(int id)
        {
            var post = _context.TblPosts.AsNoTracking().SingleOrDefault(x=>x.PostId == id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaiVietLienQuan = _context.TblPosts.AsNoTracking().Where(x=>x.Published==true && x.PostId!=id).Take(3).OrderByDescending(x=>x.CreatedDate).ToList();
            ViewBag.BaiVietLienQuan = lsBaiVietLienQuan;
            return View(post);
        }
    }
}
