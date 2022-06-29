using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarketOnlineWebsite.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly dbMarketsContext _context;

		public HomeController(ILogger<HomeController> logger, dbMarketsContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			// Khởi tạo model chính
			HomeViewVM model = new HomeViewVM();

			//Lấy ra danh sách các sản phẩm được lên trang chủ
			var lsProducts = _context.Products.AsNoTracking()
				.Where(x => x.Active == true && x.HomeFlag == true)
				.OrderByDescending(x => x.DateCreated)
				.Take(16)
				.ToList();

			// Lấy ra danh sách các danh mục sản phẩm
			List<ProductHomeVM> lsProductViews = new List<ProductHomeVM>();
			var lsCats = _context.Categories.AsNoTracking()
				.Where(x => x.Published == true)
				.OrderByDescending(x => x.Ordering)
				.ToList();

			foreach (var cat in lsCats)
			{
				ProductHomeVM productHomeVM = new ProductHomeVM();
				productHomeVM.category = cat;
				productHomeVM.lsProducts = lsProducts.Where(x => x.CatId == cat.CatId).ToList();
				lsProductViews.Add(productHomeVM);
			}

			var lsNews = _context.News.AsNoTracking()
				.Where(x => x.Active == true && x.Published == true && x.IsNewfeed == true)
				.OrderByDescending(x => x.CreatedDate)
				.Take(3)
				.ToList();
			ViewBag.AllBlogs = lsNews;
			model.Products = lsProductViews;

			ViewBag.AllProducts = lsProducts;
			return View(model);
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult About()
		{
			var lsSuppliers = _context.Suppliers.AsNoTracking()
				.Where(x => x.Active == true)
				.OrderByDescending(x => x.CreateDate)
				.Take(8)
				.ToList();

			ViewBag.LsSuppliers = lsSuppliers;
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}