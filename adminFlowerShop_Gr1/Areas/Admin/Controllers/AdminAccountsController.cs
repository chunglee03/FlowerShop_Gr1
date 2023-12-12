////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.Rendering;
////using Microsoft.EntityFrameworkCore;
////using adminFlowerShop_Gr1.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using adminFlowerShop_Gr1.Models;
//using PagedList.Core;


//namespace adminFlowerShop_Gr1.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class AdminAccountsController : Controller
//    {
//        private readonly FlowerShop_Group1Context _context;


//        public AdminAccountsController(FlowerShop_Group1Context context)
//        {
//            _context = context;

//        }
//    }

//    // GET: Admin/AdminAccounts
//    public async Task<IActionResult> Index()
//    {

//        ViewData["QuyenTruyCap"] = new SelectList(_context.TblRoles, "RoleId", "Description");

//        List<SelectListItem> IsTrangThai = new List<SelectListItem>();
//        //List<SelectListItem> IsTrangThai = new List<SelectListItem>();
//        IsTrangThai.Add(new SelectListItem() { Text = "Active", Value = "1" });
//        IsTrangThai.Add(new SelectListItem() { Text = "Block", Value = "0" });
//        ViewData["IsTrangThai"] = IsTrangThai;
//        var flowerShop_Group1Context = _context.TblAccounts.Include(t => t.Role);
//        return View(await flowerShop_Group1Context.ToListAsync());
//    }

//    // GET: Admin/AdminAccounts/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null || _context.TblAccounts == null)
//        {
//            return NotFound();
//        }

//        var tblAccount = await _context.TblAccounts
//            .Include(t => t.Role)
//            .FirstOrDefaultAsync(m => m.AccountId == id);
//        if (tblAccount == null)
//        {
//            return NotFound();
//        }

//        return View(tblAccount);
//    }

//    // GET: Admin/AdminAccounts/Create
//    public IActionResult Create()
//    {
//        ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId");
//        return View();
//    }

//    // POST: Admin/AdminAccounts/Create
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] TblAccount tblAccount)
//    {
//        if (ModelState.IsValid)
//        {
//            _context.Add(tblAccount);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId", tblAccount.RoleId);
//        return View(tblAccount);
//    }

//    // GET: Admin/AdminAccounts/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null || _context.TblAccounts == null)
//        {
//            return NotFound();
//        }

//        var tblAccount = await _context.TblAccounts.FindAsync(id);
//        if (tblAccount == null)
//        {
//            return NotFound();
//        }
//        ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId", tblAccount.RoleId);
//        return View(tblAccount);
//    }

//    // POST: Admin/AdminAccounts/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] TblAccount tblAccount)
//    {
//        if (id != tblAccount.AccountId)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                _context.Update(tblAccount);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!TblAccountExists(tblAccount.AccountId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId", tblAccount.RoleId);
//        return View(tblAccount);
//    }

//    // GET: Admin/AdminAccounts/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null || _context.TblAccounts == null)
//        {
//            return NotFound();
//        }

//        var tblAccount = await _context.TblAccounts
//            .Include(t => t.Role)
//            .FirstOrDefaultAsync(m => m.AccountId == id);
//        if (tblAccount == null)
//        {
//            return NotFound();
//        }

//        return View(tblAccount);
//    }

//    // POST: Admin/AdminAccounts/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        if (_context.TblAccounts == null)
//        {
//            return Problem("Entity set 'FlowerShop_Group1Context.TblAccounts'  is null.");
//        }
//        var tblAccount = await _context.TblAccounts.FindAsync(id);
//        if (tblAccount != null)
//        {
//            _context.TblAccounts.Remove(tblAccount);
//        }

//        await _context.SaveChangesAsync();
//        return RedirectToAction(nameof(Index));
//    }

//    private bool TblAccountExists(int id)
//    {
//        return (_context.TblAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
//    }
//}
//}

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
    public class AdminAccountsController : Controller
    {
        private readonly FlowerShop_Group1Context _context;

        public AdminAccountsController(FlowerShop_Group1Context context)
        {
            _context = context;
        }

        
        //GET: Admin/AdminAccounts
        
            public async Task<IActionResult> Index()
        {

            ViewData["QuyenTruyCap"] = new SelectList(_context.TblRoles, "RoleId", "Description");

            List<SelectListItem> IsTrangThai = new List<SelectListItem>();
            
            IsTrangThai.Add(new SelectListItem() { Text = "Active", Value = "1" });
            IsTrangThai.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["IsTrangThai"] = IsTrangThai;
            var flowerShop_Group1Context = _context.TblAccounts.Include(t => t.Role);
            return View(await flowerShop_Group1Context.ToListAsync());
        }

        // GET: Admin/AdminAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblAccounts == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.TblAccounts
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (tblAccount == null)
            {
                return NotFound();
            }

            return View(tblAccount);
        }

        // GET: Admin/AdminAccounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] TblAccount tblAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId", tblAccount.RoleId);
            return View(tblAccount);
        }

        // GET: Admin/AdminAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblAccounts == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.TblAccounts.FindAsync(id);
            if (tblAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId", tblAccount.RoleId);
            return View(tblAccount);
        }

        // POST: Admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] TblAccount tblAccount)
        {
            if (id != tblAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblAccountExists(tblAccount.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.TblRoles, "RoleId", "RoleId", tblAccount.RoleId);
            return View(tblAccount);
        }

        // GET: Admin/AdminAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblAccounts == null)
            {
                return NotFound();
            }

            var tblAccount = await _context.TblAccounts
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (tblAccount == null)
            {
                return NotFound();
            }

            return View(tblAccount);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblAccounts == null)
            {
                return Problem("Entity set 'FlowerShop_Group1Context.TblAccounts'  is null.");
            }
            var tblAccount = await _context.TblAccounts.FindAsync(id);
            if (tblAccount != null)
            {
                _context.TblAccounts.Remove(tblAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblAccountExists(int id)
        {
            return (_context.TblAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}