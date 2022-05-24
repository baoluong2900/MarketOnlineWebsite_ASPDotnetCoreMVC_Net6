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
    public class AdminAdvertisementsController : Controller
    {
        private readonly dbMarketsContext _context;

        public AdminAdvertisementsController(dbMarketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAdvertisements
        public async Task<IActionResult> Index()
        {
            return _context.Advertisements != null ?
                        View(await _context.Advertisements.ToListAsync()) :
                        Problem("Entity set 'dbMarketsContext.Advertisements'  is null.");
        }

        // GET: Admin/AdminAdvertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.AdvertisementsId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Admin/AdminAdvertisements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminAdvertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertisementsId,SubTitle,Title,ImageBg,ImageProduct,UrlLink,Active,CreateDate")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        // GET: Admin/AdminAdvertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Admin/AdminAdvertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertisementsId,SubTitle,Title,ImageBg,ImageProduct,UrlLink,Active,CreateDate")] Advertisement advertisement)
        {
            if (id != advertisement.AdvertisementsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.AdvertisementsId))
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
            return View(advertisement);
        }

        // GET: Admin/AdminAdvertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.AdvertisementsId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Admin/AdminAdvertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Advertisements == null)
            {
                return Problem("Entity set 'dbMarketsContext.Advertisements'  is null.");
            }
            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement != null)
            {
                advertisement.Active = false;
                _context.Advertisements.Update(advertisement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return (_context.Advertisements?.Any(e => e.AdvertisementsId == id)).GetValueOrDefault();
        }
    }
}
