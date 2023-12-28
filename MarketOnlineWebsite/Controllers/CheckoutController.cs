using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MarketOnlineWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly dbMarketsContext _context;

		private List<OrderDetail> OrderDetailSave = new List<OrderDetail>();
		private Models.Order OrderSave = new Models.Order();

		public INotyfService _INotyfService { get; }

		// Config paypal
		private PayPal.Api.Payment payment;
		private IHttpContextAccessor _contextAccessor;
		IConfiguration _configuration;

		public CheckoutController(dbMarketsContext context, INotyfService inotyfService, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _context = context;
            _INotyfService = inotyfService;
			_contextAccessor = contextAccessor;
			_configuration = configuration;
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

        // GET: checkout/Index
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(string? returnUrl = null)
        {
            // Lấy giỏ hàng ra để xử lý
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            var accountID = HttpContext.Session.GetString("CustomerId");
            PurchaseVM model = new PurchaseVM();
            if (accountID != null)
            {
                var customer = _context.Customers.AsNoTracking()
                    .SingleOrDefault(x => x.CustomerId == int.Parse(accountID));
                if (customer.LocationId != null && customer.District !=null && customer.Ward !=null )
                {
                    int idTinhThanh = int.Parse(customer.LocationId.ToString());
                    int idQuanHuyen = int.Parse(customer.District.ToString());
                    int idPhuongXa = int.Parse(customer.Ward.ToString());
                    model.TinhThanh = idTinhThanh;
                    model.QuanHuyen = idQuanHuyen;
                    model.PhuongXa = idPhuongXa;
   
                    ViewData["lsLQuanHuyen"] = new SelectList(_context.Locations.Where(x => x.Levels == 2 && x.Parent == idTinhThanh).OrderByDescending(x => x.LocationId == idQuanHuyen).ThenBy(x => x.Name).ToList(), "LocationId", "Name");
                    ViewData["LsPhuongXa"] = new SelectList(_context.Locations.Where(x => x.Levels == 3 && x.Parent == idQuanHuyen).OrderByDescending(x => x.LocationId == idPhuongXa).ThenBy(x => x.Name).ToList(), "LocationId", "Name");
                }
                model.CustomerId = customer.CustomerId;
                model.FullName = customer.FullName;
                model.Email = customer.Email;
                model.Phone = customer.Phone;
                model.Address = customer.Address;
                ViewData["lsLTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderByDescending(x => x.Name).ToList(), "LocationId", "Name");
            }
              
            else
            {
                ViewData["lsLTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "LocationId", "Name");
            }
          
            ViewBag.Cart = cart;
            return View(model);
        }

        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(PurchaseVM purchaseVM)
        {
            // Lấy giỏ hàng ra để xử lý
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            var accountID = HttpContext.Session.GetString("CustomerId");
            PurchaseVM model = new PurchaseVM();

            if (accountID != null && ModelState.IsValid)
            {
                var customer = _context.Customers.AsNoTracking()
                .SingleOrDefault(x => x.CustomerId == int.Parse(accountID));

                model.CustomerId = customer.CustomerId;
                model.FullName = customer.FullName;
                model.Email = customer.Email;
                model.Phone = customer.Phone;
                customer.Address = purchaseVM.Address;
                customer.LocationId = purchaseVM.TinhThanh;
                customer.District = purchaseVM.QuanHuyen;
                customer.Ward = purchaseVM.PhuongXa;

                _context.Update(customer);
                _context.SaveChanges();
            }

            ViewData["lsLTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderByDescending(x => x.Name).ToList(), "LocationId", "Name");
            int idTinhThanh = int.Parse(purchaseVM.TinhThanh.ToString());
            int idQuanHuyen = int.Parse(purchaseVM.QuanHuyen.ToString());
            int idPhuongXa = int.Parse(purchaseVM.PhuongXa.ToString());
            ViewData["lsLQuanHuyen"] = new SelectList(_context.Locations.Where(x => x.Levels == 2 && x.Parent == idTinhThanh).OrderByDescending(x => x.LocationId == idQuanHuyen).ThenBy(x => x.Name).ToList(), "LocationId", "Name");
            ViewData["LsPhuongXa"] = new SelectList(_context.Locations.Where(x => x.Levels == 3 && x.Parent == idQuanHuyen).OrderByDescending(x => x.LocationId == idPhuongXa).ThenBy(x => x.Name).ToList(), "LocationId", "Name");

            try
            {
                if (ModelState.IsValid)
                {
                    Models.Order order = new Models.Order();
                    order.CustomerId = model.CustomerId;
                    order.Address = purchaseVM.Address;
                    order.LocationId = purchaseVM.TinhThanh;
                    order.District = purchaseVM.QuanHuyen;
                    order.Ward = purchaseVM.PhuongXa;
                    order.PaymentId = purchaseVM.PaymentID;
                    order.OrderDate = DateTime.Now;
                    order.TransactStatusId = 14;
                    order.Deleted = false;
                    order.Paid = false; // thanh toán chưa
                    order.Note = Utilities.StripHTML(purchaseVM.Note);
                    order.TotalMoney = Convert.ToInt32(cart.Sum(x => x.TotalMoney));
					if (order.PaymentId == 4)
					{
						// Lưu trữ dữ liệu vào TempData
						string cartSaveJson = JsonConvert.SerializeObject(cart);
						TempData["CartSave"] = cartSaveJson;

						string orderSaveJson = JsonConvert.SerializeObject(order);
						TempData["OrderSave"] = orderSaveJson;

						return RedirectToAction("PaymentWithPaypal");
					}
					else
					{
						_context.Add(order);
						_context.SaveChanges();


						var orderNumber = 0;
						foreach (var item in cart)
						{
							orderNumber++;
						}
						// Tạo danh sách đơn hàng
						foreach (var item in cart)
						{
							OrderDetail orderDetail = new OrderDetail();
							orderDetail.OrderId = order.OrderId;
							orderDetail.ProductId = item.product.ProductId;
							orderDetail.Amount = item.amout;
							orderDetail.Price = item.product.Price;
							orderDetail.TotalMoney = order.TotalMoney;
							orderDetail.OrderNumber = orderNumber;
						}

						_context.SaveChanges();

						HttpContext.Session.Remove("Cart");

						_INotyfService.Success("Đơn hàng đặt thành công");

						return RedirectToAction("Success");
					}

					
				}
                else
                {
                    //HttpCookie userIdCookie = new HttpCookie("UserID");
                    //userIdCookie.Value = id.ToString();
                    //Response.Cookies.Add(userIdCookie);
                    ViewData["lsLTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderByDescending(x => x.Name).ToList(), "LocationId", "Name");
                    ViewBag.Cart = cart;
                    return View(purchaseVM);
                }
            }
            catch (Exception ex)
            {
                ViewData["lsLTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "LocationId", "Name");
                ViewBag.Cart = cart;

                return View(model);
            }

            ViewBag.Cart = cart;
            return View(model);
        }

		#region Setting Paypal

		public  ActionResult PaymentWithPaypal(string cancel = null, string blogId = "", string PayerId = "", string guid = "")
		{

			var ClientID = _configuration.GetValue<string>("PayPal:Key");
			var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
			var mode = _configuration.GetValue<string>("PayPal:mode");
			APIContext apiContext = PaypalExtension.GetAPIContext(ClientID, ClientSecret, mode);
			try
			{
				string payerId = PayerId;
				if (string.IsNullOrEmpty(payerId))
				{
					string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Checkout/PaymentWithPaypal?";
					var guidd = Convert.ToString((new Random()).Next(100000));
					guid = guidd;

					var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);
					var links = createdPayment.links.GetEnumerator();
					string paypalRedirectUrl = null;
					while (links.MoveNext())
					{
						Links links1 = links.Current;
						if (links1.rel.ToLower().Trim().Equals("approval_url"))
						{
							paypalRedirectUrl = links1.href;
						}
					}

					_contextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
					return Redirect(paypalRedirectUrl);

				}
				else
				{
					var paymentId = _contextAccessor.HttpContext.Session.GetString("payment");
					var executedPayment = ExecutedPayment(apiContext, payerId, paymentId as string);
					if (executedPayment.state.ToLower() != "approved")
					{
						_INotyfService.Error("Đặt thất bại");
						return View();
					}
					var blogIds = executedPayment.transactions[0].item_list.items[0].sku;
					// Khôi phục dữ liệu từ TempData
					string cartSaveJson = TempData["CartSave"] as string;
					List<CartItem> cartSave = JsonConvert.DeserializeObject<List<CartItem>>(cartSaveJson);


					// Khôi phục dữ liệu từ TempData
					string orderSaveJson = TempData["OrderSave"] as string;
					Models.Order orderSave = JsonConvert.DeserializeObject<Models.Order>(orderSaveJson);
					orderSave.Paid = true;
					orderSave.PaymentDate = DateTime.Now;
					_context.Add(orderSave);
					_context.SaveChanges();
					var orderNumber = 0;
					foreach (var item in cartSave)
					{
						orderNumber++;
					}
					// Tạo danh sách đơn hàng
					foreach (var item in cartSave)
					{
						OrderDetail orderDetail = new OrderDetail();
						orderDetail.OrderId = orderSave.OrderId;
						orderDetail.ProductId = item.product.ProductId;
						orderDetail.Amount = item.amout;
						orderDetail.Price = item.product.Price;
						orderDetail.TotalMoney = orderSave.TotalMoney;
						orderDetail.OrderNumber = orderNumber;
						_context.Add(orderDetail);
					}

					_context.SaveChanges();

					HttpContext.Session.Remove("Cart");

					_INotyfService.Success("Đơn hàng đặt thành công");

					return RedirectToAction("Success");
				
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private Payment ExecutedPayment(APIContext aPIContext, string payerId, string paymentId)
		{
			var paymentExecuteion = new PaymentExecution()
			{
				payer_id = payerId,
			};
			this.payment = new Payment()
			{
				id = paymentId,
			};
			return this.payment.Execute(aPIContext, paymentExecuteion);
		}
		private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
		{
			var itemList = new ItemList()
			{
				items = new List<Item>()
			};

			itemList.items.Add(new Item()
			{
				name = "item Detail",
				currency = "USD",
				price = "5.00",
				quantity = "1",
				sku = "asd",

			});

			var payer = new Payer()
			{
				payment_method = "paypal"
			};

			var redirUrls = new RedirectUrls()
			{
				cancel_url = redirectUrl + "&Cancel=true",
				return_url = redirectUrl
			};
			var amount = new Amount()
			{
				currency = "USD",
				total = "5.00",
			};
			var transactionList = new List<Transaction>();
			transactionList.Add(new Transaction()
			{
				description = "Transaction description",
				invoice_number = Guid.NewGuid().ToString(),
				amount = amount,
				item_list = itemList
			});
			this.payment = new Payment()
			{
				intent = "sale",
				payer = payer,
				transactions = transactionList,
				redirect_urls = redirUrls
			};
			return this.payment.Create(apiContext);
		}

		#endregion

		[Route("dat-hang-thanh-cong.html", Name = "Success")]
        public IActionResult Success()
        {
            try
            {
                var accountID = HttpContext.Session.GetString("CustomerId");
                if (string.IsNullOrEmpty(accountID))
                {
                    return RedirectToAction("Login", "Accounts", new { returnUrl = "/dat-hang-thanh-cong.html" });
                }
                var customer = _context.Customers.AsNoTracking()
                    .SingleOrDefault(x => x.CustomerId == int.Parse(accountID));
                var order = _context.Orders
                    .Where(x => x.CustomerId == int.Parse(accountID))
                    .OrderByDescending(x => x.OrderDate).FirstOrDefault();

                PurchaseSuccessVM successVM = new PurchaseSuccessVM();
                successVM.FullName = customer.FullName;
                successVM.OrderID = order.OrderId;
                successVM.Phone = customer.Phone;
                successVM.Address = order.Address;
                successVM.PhuongXa = GetNameLocation(order.Ward.Value);
                successVM.QuanHuyen = GetNameLocation(order.District.Value);
                successVM.TinhThanh = GetNameLocation(order.LocationId.Value);
                return View(successVM);
            }
            catch
            {
                return View();
            }
            return View();
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