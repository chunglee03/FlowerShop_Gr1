using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using adminFlowerShop_Gr1.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private readonly FlowerShop_Group1Context _context;
        public INotyfService _notifyService { get; }

        public AdminRolesController(FlowerShop_Group1Context context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminRoles
        public async Task<IActionResult> Index()
        {
              return _context.TblRoles != null ? 
                          View(await _context.TblRoles.ToListAsync()) :
                          Problem("Entity set 'FlowerShop_Group1Context.TblRoles'  is null.");
        }

        // GET: Admin/AdminRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblRoles == null)
            {
                return NotFound();
            }

            var tblRole = await _context.TblRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (tblRole == null)
            {
                return NotFound();
            }

            return View(tblRole);
        }

        // GET: Admin/AdminRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Description")] TblRole tblRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblRole);
        }

        // GET: Admin/AdminRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblRoles == null)
            {
                return NotFound();
            }

            var tblRole = await _context.TblRoles.FindAsync(id);
            if (tblRole == null)
            {
                return NotFound();
            }
            return View(tblRole);
        }

        // POST: Admin/AdminRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,Description")] TblRole tblRole)
        {
            if (id != tblRole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblRole);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRoleExists(tblRole.RoleId))
                    {
                        return NotFound();
                        _notifyService.Success("Có lỗi");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblRole);
        }

        // GET: Admin/AdminRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblRoles == null)
            {
                return NotFound();
            }

            var tblRole = await _context.TblRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (tblRole == null)
            {
                return NotFound();
            }

            return View(tblRole);
        }

        // POST: Admin/AdminRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblRoles == null)
            {   
                return Problem("Entity set 'FlowerShop_Group1Context.TblRoles'  is null.");
            }
            var tblRole = await _context.TblRoles.FindAsync(id);
            if (tblRole != null)
            {
                _context.TblRoles.Remove(tblRole);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa quyền truy cập thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TblRoleExists(int id)
        {
          return (_context.TblRoles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
