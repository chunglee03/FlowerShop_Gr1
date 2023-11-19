using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using adminFlowerShop_Gr1.Models;
using PagedList.Core;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTblPagesController : Controller
    {
        private readonly FlowerShop_Group1Context _context;

        public AdminTblPagesController(FlowerShop_Group1Context context)
        {
            _context = context;
        }

        // GET: Admin/AdminTblPages
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsPages = _context.TblPages
                .AsNoTracking()
                .OrderByDescending(x => x.PageId);
            PagedList<TblPage> models = new PagedList<TblPage>(IsPages, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }
        // GET: Admin/AdminTblPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblPages == null)
            {
                return NotFound();
            }

            var tblPage = await _context.TblPages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (tblPage == null)
            {
                return NotFound();
            }

            return View(tblPage);
        }

        // GET: Admin/AdminTblPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTblPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] TblPage tblPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblPage);
        }

        // GET: Admin/AdminTblPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblPages == null)
            {
                return NotFound();
            }

            var tblPage = await _context.TblPages.FindAsync(id);
            if (tblPage == null)
            {
                return NotFound();
            }
            return View(tblPage);
        }

        // POST: Admin/AdminTblPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] TblPage tblPage)
        {
            if (id != tblPage.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPageExists(tblPage.PageId))
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
            return View(tblPage);
        }

        // GET: Admin/AdminTblPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblPages == null)
            {
                return NotFound();
            }

            var tblPage = await _context.TblPages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (tblPage == null)
            {
                return NotFound();
            }

            return View(tblPage);
        }

        // POST: Admin/AdminTblPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblPages == null)
            {
                return Problem("Entity set 'FlowerShop_Group1Context.TblPages'  is null.");
            }
            var tblPage = await _context.TblPages.FindAsync(id);
            if (tblPage != null)
            {
                _context.TblPages.Remove(tblPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPageExists(int id)
        {
          return (_context.TblPages?.Any(e => e.PageId == id)).GetValueOrDefault();
        }
    }
}
