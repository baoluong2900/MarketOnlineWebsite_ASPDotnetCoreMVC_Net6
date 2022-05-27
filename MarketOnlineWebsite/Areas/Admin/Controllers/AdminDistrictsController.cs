using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketOnlineWebsite.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using PagedList.Core;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDistrictsController : Controller
    {
        private readonly dbMarketsContext _context;
        private const int idLocations = 1;
        private const int idDistricts = 2;
        public INotyfService _INotyfService { get; }
        public AdminDistrictsController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        // GET: Admin/AdminLocations
        public async Task<IActionResult> Index(int? page)
        {
            // xử lý phân trang
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsLocations = _context.Locations.AsNoTracking().Where(x => x.Levels == idDistricts);

            PagedList<Location> models = new PagedList<Location>(lsLocations, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            var lsGetNameLocation = _context.Locations.AsNoTracking()
              .Where(x => x.Levels == idLocations)
              .ToList();

            ViewBag.LsLocation = lsGetNameLocation;
            return View(models);
        }

        // GET: Admin/AdminDistricts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var lsGetNameLocation = _context.Locations.AsNoTracking()
                                    .Where(x => x.Levels == idLocations)
                                    .ToList();

            ViewBag.LsLocation = lsGetNameLocation;
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Admin/AdminDistricts/Create
        public IActionResult Create()
        {
            ViewData["LsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1), "LocationId", "Name");
            return View();
        }

        // POST: Admin/AdminDistricts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,Name,Parent,Levels,Slug,NameWithType,Type")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.Levels = idDistricts;
                _context.Add(location);
                await _context.SaveChangesAsync();
                _INotyfService.Success("Thêm thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LsTinhThanh"] = new SelectList(_context.Locations.Where(x=>x.Levels==1), "LocationId", "Name");
            return View(location);
        }

        // GET: Admin/AdminDistricts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["LsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1), "LocationId", "Name");
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Admin/AdminDistricts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,Name,Parent,Levels,Slug,NameWithType,Type")] Location location)
        {
            ViewData["LsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1), "LocationId", "Name");
            if (id != location.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    location.Levels = 2;
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                    _INotyfService.Success("Chỉnh sửa thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationId))
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
            return View(location);
        }

        // GET: Admin/AdminDistricts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            var lsGetNameLocation = _context.Locations.AsNoTracking()
                                   .Where(x => x.Levels == idLocations)
                                   .ToList();

            ViewBag.LsLocation = lsGetNameLocation;
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Admin/AdminDistricts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locations == null)
            {
                return Problem("Entity set 'dbMarketsContext.Locations'  is null.");
            }
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                
                _context.Locations.Remove(location);
            }
            
            await _context.SaveChangesAsync();
            _INotyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
          return (_context.Locations?.Any(e => e.LocationId == id)).GetValueOrDefault();
        }
    }
}
