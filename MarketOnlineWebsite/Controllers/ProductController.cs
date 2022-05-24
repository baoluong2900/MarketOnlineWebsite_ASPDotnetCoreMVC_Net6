using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace MarketOnlineWebsite.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// Sắp xếp theo Mặc định
        /// </summary>
        private const int SORT_VALUE_1 = 1;

        /// <summary>
        /// Sắp xếp theo độ phổ biến
        /// </summary>
        private const int SORT_VALUE_2 = 2;

        /// <summary>
        /// Sắp xếp theo khuyến mãi
        /// </summary>
        private const int SORT_VALUE_3 = 3;

        /// <summary>
        /// Sắp xếp theo giá giảm dần
        /// </summary>
        private const int SORT_VALUE_4 = 4;
        /// <summary>
        /// Sắp xếp theo giá tăng dần
        /// </summary>
        private const int SORT_VALUE_5 = 5;


        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }
        public ProductController(dbMarketsContext context, INotyfService inotyfService )
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        // GET: Product
        [HttpGet]
        [Route("product.html", Name = "Product")]
        public IActionResult Index(int? page, string searchProductNamme)
        {
            try
            {
               
                //ViewData["CurrentFilter"] = searchProductNamme;
                ViewBag.CurrentFilter = searchProductNamme;
                var lsProducts = _context.Products.AsNoTracking().Where(x=> x.Active == true).OrderByDescending(x=>x.DateCreated);
                if (!string.IsNullOrEmpty(searchProductNamme))
                {
                    lsProducts = (IOrderedQueryable<Product>)lsProducts.Where(x => x.ProductName.Contains(searchProductNamme) && x.Active == true);
                  
                }
                var pageNumber = page == null || page < 0 ? 1 : page.Value;
                var pageSize = 12;
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewBag.Count = lsProducts.Count();
                var topProducts = _context.Products.AsNoTracking().Where(x => x.HomeFlag == true && x.Active == true && x.BestSellers == true).OrderByDescending(x => x.DateCreated).Take(3).ToList();
                ViewBag.TopProducts = topProducts;
                return View(models);
            }
            catch 
            {

                return RedirectToAction("Index", "Home");

            }

        }
        //[HttpPost]
        //[Route("product.html", Name = "Product")]
        //public IActionResult Index(int? page,int? data)
        //{
        //   try
        //    {
        //        var pageNumber = page == null || page < 0 ? 1 : page.Value;
        //        var pageSize = 12;
        //        var lsProducts = _context.Products.AsNoTracking();
        //        if(data == SORT_VALUE_4)
        //        {
        //            lsProducts = lsProducts.Where(x=>x.Price==1).OrderByDescending(x=>x.Price);
        //        }
        //        PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
        //        ViewBag.CurrentPage = pageNumber;
        //        return View(models);

        //if (data == SORT_VALUE_2)
        //{
        //    var lsProducts = _context.Products.AsNoTracking().OrderByDescending(x => x.HomeFlag == true);
        //    models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
        //    return View(models);

        //}
        //else if (data == SORT_VALUE_3)
        //{
        //    var lsProducts = _context.Products.AsNoTracking().OrderByDescending(x => x.BestSellers == true);
        //    models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
        //    return View(models);

        //}
        //else if ( data == SORT_VALUE_4)
        //{
        //    var lsProducts = _context.Products.AsNoTracking().OrderByDescending(x => x.Price);
        //     models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
        //    return View(models);

        //}
        //else if (data == SORT_VALUE_5)
        //{
        //    var lsProducts = _context.Products.AsNoTracking().OrderBy(x => x.Price);
        //    models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
        //    return View(models);

        //

        //    } 
        //   catch
        //   {
        //        return RedirectToAction("Index", "Home");
        //   }

        //}

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
                var topProducts = _context.Products.AsNoTracking().Where(x => x.HomeFlag == true && x.Active == true && x.BestSellers == true).OrderByDescending(x => x.DateCreated).Take(3).ToList();
                ViewBag.TopProducts = topProducts;
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
