using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MarketOnlineWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }

        public CheckoutController(dbMarketsContext context, INotyfService inotyfService)
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
                    Order order = new Order();
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
                        _context.Add(orderDetail);
                    }

                    _context.SaveChanges();

                    HttpContext.Session.Remove("Cart");

                    _INotyfService.Success("Đơn hàng đặt thành công");

                    return RedirectToAction("Success");
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