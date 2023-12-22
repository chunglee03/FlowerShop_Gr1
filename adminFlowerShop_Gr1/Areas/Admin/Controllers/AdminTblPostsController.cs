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
    public class AdminTblPostsController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public INotyfService _notifyService { get; }

        public AdminTblPostsController(FlowerShop_Group1Context context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminTblPosts
        public IActionResult Index(int? page)
        {
            var collection = _context.TblPosts.ToList();
            foreach (var item in collection)
            {
                if (item.CreatedDate == null) {
                    item.CreatedDate = DateTime.Now;
                    _context.Update(item);
                    _context.SaveChanges();
                }
            }
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var tblPosts = _context.TblPosts
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<TblPost> models = new PagedList<TblPost>(tblPosts, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }
        // GET: Admin/AdminTblPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblPosts == null)
            {
                return NotFound();
            }

            var tblPost = await _context.TblPosts
                .Include(t => t.Account)
                .Include(t => t.Cat)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tblPost == null)
            {
                return NotFound();
            }

            return View(tblPost);
        }

        // GET: Admin/AdminTblPosts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId");
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId");
            return View();
        }

        // POST: Admin/AdminTblPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewFeed,MetaKey,MetaDesc,Views")] TblPost tblPost, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            // xu ly thumb
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(tblPost.Title) + extension;
                    tblPost.Thumb = await Utilities.UploadFile(fThumb, @"post", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(tblPost.Thumb)) tblPost.Thumb = "default.jpg";
                tblPost.Alias = Utilities.SEOUrl(tblPost.Title);
                tblPost.CreatedDate = DateTime.Now;

                _context.Add(tblPost);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm mới thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId", tblPost.AccountId);
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId", tblPost.CatId);
            return View(tblPost);
        }

        // GET: Admin/AdminTblPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblPosts == null)
            {
                return NotFound();
            }

            var tblPost = await _context.TblPosts.FindAsync(id);
            if (tblPost == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId", tblPost.AccountId);
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId", tblPost.CatId);
            return View(tblPost);
        }

        // POST: Admin/AdminTblPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewFeed,MetaKey,MetaDesc,Views")] TblPost tblPost, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != tblPost.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(tblPost.Title) + extension;
                        tblPost.Thumb = await Utilities.UploadFile(fThumb, @"post", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(tblPost.Thumb)) tblPost.Thumb = "default.jpg";
                    tblPost.Alias = Utilities.SEOUrl(tblPost.Title);

                    _context.Update(tblPost);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPostExists(tblPost.PostId))
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
            ViewData["AccountId"] = new SelectList(_context.TblAccounts, "AccountId", "AccountId", tblPost.AccountId);
            ViewData["CatId"] = new SelectList(_context.TblCategories, "CatId", "CatId", tblPost.CatId);
            return View(tblPost);
        }

        // GET: Admin/AdminTblPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblPosts == null)
            {
                return NotFound();
            }

            var tblPost = await _context.TblPosts
                .Include(t => t.Account)
                .Include(t => t.Cat)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tblPost == null)
            {
                return NotFound();
            }

            return View(tblPost);
        }

        // POST: Admin/AdminTblPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblPosts == null)
            {
                return Problem("Entity set 'FlowerShop_Group1Context.TblPosts'  is null.");
            }
            var tblPost = await _context.TblPosts.FindAsync(id);
            if (tblPost != null)
            {
                _context.TblPosts.Remove(tblPost);
            }

            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TblPostExists(int id)
        {
            return (_context.TblPosts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
