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
    public class AdminCustomersController : Controller
    {
        private readonly FlowerShop_Group1Context _context;

        public AdminCustomersController(FlowerShop_Group1Context context)
        {
            _context = context;
        }

        // GET: Admin/AdminCustomers
        //public async Task<IActionResult> Index()
        //{
        //    var flowerShop_Group1Context = _context.TblCustomers.Include(t => t.Location);
        //    return View(await flowerShop_Group1Context.ToListAsync());
        //}
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsCustomers = _context.TblCustomers
                .AsNoTracking()
                .Include(x => x.Location)
                .OrderByDescending(x => x.CreateDate);
            PagedList<TblCustomer> models = new PagedList<TblCustomer>(IsCustomers, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }

        public IActionResult Filtter(int CustomerId = 0)
        {

            var url = $"/Admin/AdminCustomers?CustomerId={CustomerId}";
            if (CustomerId == 0)
            {
                url = $"/Admin/AdminCustomers";
            }
            return Json(new { status = "success", RedirectUrl = url });
        }
        // GET: Admin/AdminCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers
                .Include(t => t.Location)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // GET: Admin/AdminCustomers/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Name");
            return View();
        }

        // POST: Admin/AdminCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FullName,Birthday,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] TblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Name", tblCustomer.LocationId);
            return View(tblCustomer);
        }

        // GET: Admin/AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers.FindAsync(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Name", tblCustomer.LocationId);
            return View(tblCustomer);
        }

        // POST: Admin/AdminCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FullName,Birthday,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] TblCustomer tblCustomer)
        {
            if (id != tblCustomer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCustomerExists(tblCustomer.CustomerId))
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
            ViewData["LocationId"] = new SelectList(_context.TblLocations, "LocationId", "Name", tblCustomer.LocationId);
            return View(tblCustomer);
        }

        // GET: Admin/AdminCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers
                .Include(t => t.Location)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // POST: Admin/AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblCustomers == null)
            {
                return Problem("Entity set 'FlowerShop_Group1Context.TblCustomers'  is null.");
            }
            var tblCustomer = await _context.TblCustomers.FindAsync(id);
            if (tblCustomer != null)
            {
                _context.TblCustomers.Remove(tblCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCustomerExists(int id)
        {
          return (_context.TblCustomers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
