using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace MarketOnlineWebsite.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly dbMarketsContext _context;
		public INotyfService _INotyfService { get; }
		private const string coupon_1 = "GIAM10%";
		private const string coupon_2 = "UUDAI10%";

		public ShoppingCartController(dbMarketsContext context, INotyfService inotyfService)
		{
			_context = context;
			_INotyfService = inotyfService;
		}

		#region Hàm lấy giỏ hàng

		public List<CartItem> Cart
		{
			get
			{
				var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

				// Nếu chưa có giỏ hàng thì khởi tạo giỏi hàng
				if (cart == default(List<CartItem>))
				{
					cart = new List<CartItem>();
				}

				// Nếu có giỏ hàng trả về giỏ hàng
				return cart;
			}
		}

		#endregion Hàm lấy giỏ hàng

		#region Hàm thêm mới sản phẩm vào giỏ hàng

		[HttpPost]
		[Route("api/cart/add")]
		public IActionResult AddToCart(int productID, int? amount)
		{
			List<CartItem> cart = Cart;
			try
			{
				CartItem item = Cart.SingleOrDefault(p => p.product.ProductId == productID);

				var carts = HttpContext.Session.Get<List<CartItem>>("Cart");
				if (item != null)
				{
					if (carts != null)
					{
						CartItem items = carts.SingleOrDefault(p => p.product.ProductId == productID);
						if (items != null && amount.HasValue)
						{
							items.amout += amount.Value;
						}

						// lưu lại session
						HttpContext.Session.Set<List<CartItem>>("Cart", carts);
					}
				}
				else
				{
					Product products = _context.Products.SingleOrDefault(p => p.ProductId == productID);
					item = new CartItem
					{
						amout = amount.HasValue ? amount.Value : 1,
						product = products,
					};
					//if (itemCheck.discount.ToString() != null)
					//{
					//    item.discount = itemCheck.discount;
					//}

					// Thêm vào giỏ hàng
					cart.Add(item);
					HttpContext.Session.Set<List<CartItem>>("Cart", cart);
				}

				// Lưu lại session

				_INotyfService.Success("Thêm sản phẩm thành công");
				//   return RedirectToAction("Index");
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		#endregion Hàm thêm mới sản phẩm vào giỏ hàng

		#region Hàm xóa luôn tất cả sản phẩm khỏi giỏ hàng

		[HttpPost]
		[Route("api/cart/remove")]
		public ActionResult Remove(int productID)
		{
			try
			{
				List<CartItem> cart = Cart;
				CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);

				if (item != null)
				{
					cart.Remove(item);
				}

				// Lưu lại session
				HttpContext.Session.Set<List<CartItem>>("Cart", cart);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		#endregion Hàm xóa luôn tất cả sản phẩm khỏi giỏ hàng

		[HttpPost]
		[Route("api/cart/removeall")]
		public ActionResult RemoveAll()
		{
			try
			{
				List<CartItem> cart = Cart;
				HttpContext.Session.Remove("Cart");
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		#region Hàm cập nhật lại số lượng sản phẩm trong giỏ hàng

		[HttpPost]
		[Route("api/cart/update")]
		public IActionResult UpdateCart(int productID, int? amount)
		{
			// Lấy giỏ hàng ra để xử lý
			var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
			try
			{
				// Nếu đã có thì cập nhật lại số lượng
				if (cart != null)
				{
					CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
					if (item != null && amount.HasValue)
					{
						item.amout = amount.Value;
					}

					// lưu lại session
					HttpContext.Session.Set<List<CartItem>>("Cart", cart);
				}
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		#endregion Hàm cập nhật lại số lượng sản phẩm trong giỏ hàng

		#region Hàm cập nhật lại giảm giá

		[HttpPost]
		[Route("api/cart/discount")]
		public IActionResult UpdateCartDiscount(string? discount)
		{
			// Lấy giỏ hàng ra để xử lý
			var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
			try
			{
				double test = 0;
				// Nếu đã có thì cập nhật lại số lượng
				if (cart != null)
				{
					CartItem item = cart.SingleOrDefault();
					//item.TotalMoney = item.TotalMoney *((ushort)double.Parse(discount);

					if (item != null && checkDiscount(discount))
					{
						item.discount = discount;
						_INotyfService.Success("Áp dụng mã thành công");
					}
					else
					{
						ModelState.AddModelError("discount", "Email này đã tồn tại rồi");
					}

					// lưu lại session
					HttpContext.Session.Set<List<CartItem>>("Cart", cart);
				}
				_INotyfService.Success("Áp dụng mã thành công");
				return Json(new { success = true });
			}
			catch
			{
				_INotyfService.Warning("Mã không hợp lệ");
				return Json(new { success = false });
			}
		}

		#endregion Hàm cập nhật lại giảm giá

		public Boolean checkDiscount(string discount)
		{
			if (discount == coupon_1 && !string.IsNullOrEmpty(discount))
			{
				return true;
			}
			else if (discount == coupon_2 && !string.IsNullOrEmpty(discount))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		[Route("cart.html", Name = "Cart")]
		public IActionResult Index()
		{
			return View(Cart);
		}
	}
}