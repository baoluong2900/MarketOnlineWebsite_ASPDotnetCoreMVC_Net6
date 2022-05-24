#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketOnlineWebsite.Models;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCustomersController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }

        public AdminCustomersController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        // GET: Admin/AdminCustomers
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsCustomers = _context.Customers.AsNoTracking().Include(x => x.Location).OrderByDescending(x => x.CustomerId);
            PagedList<Customer> models = new PagedList<Customer>(lsCustomers, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            //var dbMarketsContext = _context.Customers.Include(c => c.Location);
            //return View(await dbMarketsContext.ToListAsync());



            return View(models);
        }

        // GET: Admin/AdminCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            string TinhThanh = GetNameLocation(customer.LocationId.Value);
            string QuanHuyen = GetNameLocation(customer.District.Value);
            string PhuongXa = GetNameLocation(customer.Ward.Value);
            string addressLoaction = $"{TinhThanh}, {QuanHuyen}, {PhuongXa}";
            ViewBag.AddressLoaction = addressLoaction;
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/AdminCustomers/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: Admin/AdminCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FullName,Birthday,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", customer.LocationId);
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", customer.LocationId);
            return View(customer);
        }

        // POST: Admin/AdminCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FullName,Birthday,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", customer.LocationId);
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            customer.Active = false;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        public string GetNameLocation(int idLocation)
        {
            try
            {
                string nameFetchedId = _context.Locations.SingleOrDefault(x => x.LocationId == idLocation)?.Name;
                return nameFetchedId;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
