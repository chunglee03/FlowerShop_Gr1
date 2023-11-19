using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using adminFlowerShop_Gr1.Models;
using PagedList.Core;
using adminFlowerShop_Gr1.Helpper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public INotyfService _notifyService { get; }

        private object fThumb;
        private object fThum;
        private string extension;

        public AdminProductsController(FlowerShop_Group1Context context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminProducts
        //public async Task<IActionResult> Index()
        //{
        //    var flowerShop_Group1Context = _context.TblProducts.Include(t => t.Cat);
        //    return View(await flowerShop_Group1Context.ToListAsync());
        //}
        public IActionResult Index(int page=1,int CatID=0)
        {
            var pageNumber = page;
            var pageSize = 20;



            List<TblProduct> IsProducts = new List<TblProduct>();
            if (CatID != 0)
            {
                IsProducts = _context.TblProducts
                                .AsNoTracking(). Where(x=>x.CatId==CatID)
                                .Include(x => x.Cat)
                                .OrderByDescending(x => x.ProductId).ToList();
            }
            else
            {
                IsProducts = _context.TblProducts
                                .AsNoTracking()
                                .Include(x => x.Cat)
                                .OrderByDescending(x => x.ProductId).ToList();
            }
             
            PagedList<TblProduct> models = new PagedList<TblProduct>(IsProducts.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID = CatID;
            ViewBag.CurrentPage = pageNumber;

			ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CatId", "CatName", CatID);
			return View(models);

        }


        public IActionResult Filtter(int CatID = 0)
        {
           
            var url = $"/Admin/AdminProducts?CatID={CatID}";
            if (CatID ==0 )
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { status = "success", RedirectUrl = url });
        }
        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts
                .Include(t => t.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            return View(tblProduct);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatName");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] TblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                tblProduct.ProductName = Utilities.ToTitlecase(tblProduct.ProductName);
                if (fThumb != null)
                { 
                    
                    string image = Utilities.SEOUrl(tblProduct.ProductName) + extension;
                    tblProduct.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                }
                if (string.IsNullOrEmpty(tblProduct.Thumb)) tblProduct.Thumb = "default.jpg";
                tblProduct.Alias = Utilities.SEOUrl(tblProduct.ProductName);
                tblProduct.DateModified = DateTime.Now;
                tblProduct.DateCreated = DateTime.Now;
                _context.Add(tblProduct);
                await _context.SaveChangesAsync();
                _notifyService.Success("Cập nhật thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CatId", "CatName", tblProduct.CatId);
            return View(tblProduct);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return NotFound();
            }
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CatId", "CatName", tblProduct.CatId);
            return View(tblProduct);
            
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] TblProduct tblProduct,Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != tblProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {

                    tblProduct.ProductName = Utilities.ToTitlecase(tblProduct.ProductName);
                    if (fThumb != null)
                    {

                        string image = Utilities.SEOUrl(tblProduct.ProductName) + extension;
                        tblProduct.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(tblProduct.Thumb)) tblProduct.Thumb = "default.jpg";
                    tblProduct.Alias = Utilities.SEOUrl(tblProduct.ProductName);
                    tblProduct.DateModified = DateTime.Now;
                    _context.Update(tblProduct);
                    _notifyService.Success("Cập nhật thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblProductExists(tblProduct.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.TblCategories, "CatId", "CatName", tblProduct.CatId);
            return View(tblProduct);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts
                .Include(t => t.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            return View(tblProduct);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblProducts == null)
            {
                return Problem("Entity set 'FlowerShop_Group1Context.TblProducts'  is null.");
            }
            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct != null)
            {
                _context.TblProducts.Remove(tblProduct);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TblProductExists(int id)
        {
          return (_context.TblProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
