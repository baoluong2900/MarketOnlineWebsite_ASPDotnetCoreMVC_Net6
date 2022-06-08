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
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Extension;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }
        public AdminProductsController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index(int? page, int CatID=0)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 20;
            //     var lsProducts = _context.Products.AsNoTracking().Include(x => x.Cat).OrderByDescending(x => x.ProductId);
            List<Product> lsProducts = new List<Product>();
            if (CatID != 0)
            {
                lsProducts = _context.Products
                                            .AsNoTracking()
                                            .Where(x=>x.CatId == CatID && x.Active == true)
                                            .Include(x => x.Cat)
                                            .OrderByDescending(x => x.ProductId).ToList();
            }
            else
            {
                lsProducts = _context.Products
                                          .AsNoTracking()
                                          .Include(x => x.Cat)
                                          .Where(x => x.Active == true)
                                          .OrderByDescending(x => x.ProductId).ToList();
            }
            PagedList<Product> models = new PagedList<Product>(lsProducts.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID= CatID;
            ViewBag.CurrentPage = pageNumber;
            // Lọc theo danh mục sản phẩm
            ViewData["DanhMucSanPham"] = new SelectList(_context.Categories, "CatId", "CatName",CatID);
            //var dbMarketsContext = _context.Customers.Include(c => c.Location);
            //return View(await dbMarketsContext.ToListAsync());
            return View(models);
        }
        public IActionResult Fillter(int CatID = 0)
        {
            var url = $"/Admin/AdminProducts?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { status = "success", redirectUrl = url });

        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["DanhMucSanPham"] = new SelectList(_context.Categories, "CatId", "CatName");
            ViewData["LsNhaCungCap"] = new SelectList(_context.Suppliers, "SupplierId", "Companyname");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,UnitsInStock,SupplierId")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                product.ProductName = Utilities.ToTitleCase(product.ProductName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(product.ProductName) + extension;
                    product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                }
              
                if (string.IsNullOrEmpty(product.Thumb)) product.Thumb = "default.jpg";
                product.Alias = Utilities.SEOUrl(product.ProductName);
                product.DateModified = DateTime.Now;

                _context.Add(product);
                await _context.SaveChangesAsync();
                _INotyfService.Success("Thêm thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucSanPham"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            ViewData["LsNhaCungCap"] = new SelectList(_context.Suppliers, "SupplierId", "Companyname", product.SupplierId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DanhMucSanPham"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            ViewData["LsNhaCungCap"] = new SelectList(_context.Suppliers, "SupplierId", "Companyname", product.SupplierId);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,UnitsInStock,SupplierId")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.ProductName = Utilities.ToTitleCase(product.ProductName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                    }

                    if (string.IsNullOrEmpty(product.Thumb)) product.Thumb = "default.jpg";
                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.DateModified = DateTime.Now;
                    _context.Update(product);
                    _INotyfService.Success("Cập nhật thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        _INotyfService.Error("Cập nhật thất bại");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LsNhaCungCap"] = new SelectList(_context.Suppliers, "SupplierId", "Companyname", product.SupplierId);
            ViewData["DanhMucSanPham"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            product.Active = false;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            _INotyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }


    }
}
