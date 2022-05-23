using MarketOnlineWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly dbMarketsContext _context;
        public SearchController(dbMarketsContext context)
        {
            _context = context;
        }

        //GET: Search/FindProduct
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Product> ls = new List<Product>();
            var lsProduct = _context.Products
                                      .AsNoTracking()
                                      .Include(x => x.Cat)
                                      .ToList();
            if ( string.IsNullOrEmpty( keyword ) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", lsProduct);
            }
            ls = _context.Products
                                            .AsNoTracking()
                                            .Include(x=>x.Cat)
                                            .Where(x=>x.ProductName.Contains(keyword.Trim()))
                                            .OrderByDescending(x=>x.ProductName)
                                            .Take(10)
                                            .ToList();
            if(ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }

        //GET: Search/FindProduct
        [HttpPost]
        public IActionResult FindCustomer(string keyword)
        {
            List<Customer> ls = new List<Customer>();
            var lsCustomer = _context.Customers
                                      .AsNoTracking()
                                      .ToList();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
               
                return PartialView("ListCustomersSearchPartial", lsCustomer);
            }
            ls = _context.Customers
                                            .AsNoTracking()
                                            .Where(x => x.FullName.Contains(keyword.Trim()))
                                            .OrderByDescending(x => x.FullName)
                                            .Take(10)
                                            .ToList();
            if (ls == null)
            {
                return PartialView("ListCustomersSearchPartial", null);
            }
            else
            {
                return PartialView("ListCustomersSearchPartial", ls);
            }
        }
    }
}
