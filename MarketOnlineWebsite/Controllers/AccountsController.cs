using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarketOnlineWebsite.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }
        public AccountsController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var customer=_context.Customers.AsNoTracking()
                    .SingleOrDefault(x=>x.Phone.ToLower() == Phone.Trim().ToLower());
                if (customer !  == null)
                {
                    return Json(data: "Số điện thoại : " + Phone + " Đã được sử dụng ");

                }
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var customer = _context.Customers.AsNoTracking()
                    .SingleOrDefault(x => x.Email.ToLower() == Email.Trim().ToLower());
                if (customer! == null)
                {
                    return Json(data: "Email : " + Email + " Đã được sử dụng<br />");

                }
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }

        [Route("my-account.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var acountID = HttpContext.Session.GetString("CustomerId");
            if (acountID != null)
            {
               var customer = _context.Customers.AsNoTracking()
                    .SingleOrDefault(x=>x.CustomerId == Convert.ToInt32(acountID.Trim()));
                if (customer != null)
                {
                    var lsOrder = _context.Orders.AsNoTracking()
                        .Include(x=>x.TransactStatus)
                        .Where(x=>x.CustomerId== customer.CustomerId)
                        .OrderByDescending(x=>x.OrderDate).ToList();
                    ViewBag.Order = lsOrder;
                    return View(customer);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html",Name ="DangKy")]
        public IActionResult RegisterAccount()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> RegisterAccount(RegisterVM Accounts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Customer customer = new Customer
                    {
                        FullName = Accounts.FullName,
                        Phone = Accounts.Phone.Trim().ToLower(),
                        Email = Accounts.Email.Trim().ToLower(),
                        Password = (Accounts.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt

                    };
                    try
                    {
                        _context.Add(customer);
                        await _context.SaveChangesAsync();
                        // Lưu session MaKH
                        HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
                        var accountID = HttpContext.Session.GetString("CustomerId");
                        //Indetity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, customer.FullName),
                            new Claim("CustomerId", customer.CustomerId.ToString()),
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _INotyfService.Success("Đăng ký thành công");
                        return RedirectToAction("Dashboard", "Accounts");

                    }
                    catch
                    {
                        return RedirectToAction("DangKyTaiKhoan", "Accounts");
                    }
                }
                else
                {
                    return View(Accounts);
                }

            }
            catch
            {
                return View(Accounts);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl = null)
        {
            var customerID = HttpContext.Session.GetString("CustomerId");
            if (customerID != null)
            {
                return RedirectToAction("Dashboard", "Account");
            }
/*            ViewBag.ReturnUrl = returnUrl;
            // Check nếu giỏ hàng đã có thì trả về giỏ hàng để khách hàng thấy
            ViewBag.ShoppingCarts = GioHang;*/
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customers.UserName);
                    if(!isEmail)
                    { 
                        return View(customers);
                    }
                    var customer = _context.Customers
                            .SingleOrDefault(x => x.Email.ToLower().Trim() == customers.UserName.ToLower().Trim());
                    if(customer == null)
                    {
                        return RedirectToAction("DangKyTaiKhoan");
                    }

                    string pass = (customers.Password + customer.Salt.Trim()).ToMD5();
                    if(customer.Password != pass)
                    {
                        _INotyfService.Error("Thông tin đăng nhập chưa chính xác");
                        return View(customers );
                    }
                    if(customer.Active == false)
                    {
                        return RedirectToAction("ThongBao", "Accounts");
                    }

                    // Lưu session MaKH
                    HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
                    var customerID = HttpContext.Session.GetString("CustomerId");
                    // Indentity

                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, customer.FullName),
                            new Claim("CustomerId", customer.CustomerId.ToString()),
                        };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _INotyfService.Success("Đăng nhập thành công"); 
                    return RedirectToAction("Dashboard", "Accounts");

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("DangKyTaiKhoan", "Accounts");
            }
            return View(customers );
        }

        [HttpGet]
        [Route("dang-xuat.html",Name ="Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }
    }
}
