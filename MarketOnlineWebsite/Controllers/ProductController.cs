using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace MarketOnlineWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }
        public ProductController(dbMarketsContext context, INotyfService inotyfService )
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        [Route("product.html", Name = "Product")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page < 0 ? 1 : page.Value;
                var pageSize = 12;
                var lsProducts = _context.Products.AsNoTracking().OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch 
            {


                return RedirectToAction("Index", "Home");

            }

        }

        [Route("/{Alias}", Name = "ListProduct")]
        public IActionResult List(string alias, int page=1)
        {
            try
            {
                var pageSize = 12;
                var lsCategories = _context.Categories.AsNoTracking().SingleOrDefault(x=> x.Alias == alias);
                var lsProducts = _context.Products.AsNoTracking().Where(x => x.CatId == lsCategories.CatId).OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = lsCategories;
                return View(models);
            }
            catch
            {
              return RedirectToAction("Index","Home");
         
            }

        }

        [Route("/{Alias}-{id}.html",Name ="ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var lsProduct = _context.Products.AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active==true)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(4)
                    .ToList();
                ViewBag.Product = lsProduct;
                return View(product);
            }
            catch
            {
                _INotyfService.Error("Có lỗi xảy ra ");
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
