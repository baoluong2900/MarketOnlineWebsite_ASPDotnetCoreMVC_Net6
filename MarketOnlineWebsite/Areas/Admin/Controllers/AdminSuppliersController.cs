using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSuppliersController : Controller
    {
        private readonly dbMarketsContext _context;

        public AdminSuppliersController(dbMarketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminSuppliers
        public async Task<IActionResult> Index()
        {
            var dbMarketsContext = _context.Suppliers.Include(s => s.Account).Include(s => s.Location);
            return View(await dbMarketsContext.ToListAsync());
        }

        // GET: Admin/AdminSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .Include(s => s.Account)
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Admin/AdminSuppliers/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: Admin/AdminSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,AccountId,EmailContact,Companyname,ContactTitle,Addresss,PostalCode,Fax,PaymentMethods,LocationId,District,Ward,DiscountType,Notes,CurrentOrder,CreateDate,Logo,Active")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", supplier.AccountId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", supplier.LocationId);
            return View(supplier);
        }

        // GET: Admin/AdminSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", supplier.AccountId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", supplier.LocationId);
            return View(supplier);
        }

        // POST: Admin/AdminSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,AccountId,EmailContact,Companyname,ContactTitle,Addresss,PostalCode,Fax,PaymentMethods,LocationId,District,Ward,DiscountType,Notes,CurrentOrder,CreateDate,Logo,Active")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", supplier.AccountId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", supplier.LocationId);
            return View(supplier);
        }

        // GET: Admin/AdminSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .Include(s => s.Account)
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Admin/AdminSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'dbMarketsContext.Suppliers'  is null.");
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
          return (_context.Suppliers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}
