//using AspNetCoreHero.ToastNotification.Abstractions;
//using MarketOnlineWebsite.Helpper;
//using MarketOnlineWebsite.Models;
//using MarketOnlineWebsite.ModelViews;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace MarketOnlineWebsite.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Authorize]
//    public class AdminAccountRoleController : Controller
//    {
//        private readonly dbMarketsContext _context;
//        public INotyfService _INotyfService { get; }
//        public AdminAccountRoleController(dbMarketsContext context, INotyfService inotyfService)
//        {
//            _context = context;
//            _INotyfService = inotyfService;
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        [Route("/Admin/dang-nhap-user.html", Name = "DangNhapUser")]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        //public IActionResult Login()
//        //{
//        //    var customerID = HttpContext.Session.GetString("CustomerId");
//        //    if (customerID != null)
//        //    {
//        //        return Redirect("my-account.html");
//        //    }

//        //    // Check nếu giỏ hàng đã có thì trả về giỏ hàng để khách hàng thấy
//        //    //ViewBag.ShoppingCarts = Cart;
//        //    return View();

//        //}

//        [HttpPost]
//        [AllowAnonymous]
//        [Route("/Admin/dang-nhap-user.html", Name = "DangNhapUser")]
//        public async Task<IActionResult> Index(LoginViewModel accounts, string? returnUrl = null)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    var account = _context.Accounts
//                            .SingleOrDefault(x => x.Email.ToLower().Trim() == accounts.UserName.ToLower().Trim() && x.Password == accounts.Password);
//                    if (account == null) {
//                        _INotyfService.Warning("Chưa tồn tại kìa");
//                        return View(accounts);
//                    }
//                    HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
//                    var accountID = HttpContext.Session.GetString("AccountId");
//                    var claims = new List<Claim>
//                        {
//                            new Claim(ClaimTypes.Name, account.FullName),
//                            new Claim("AccountId", account.AccountId.ToString()),
//                        };
//                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Index");
//                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//                    await HttpContext.SignInAsync(claimsPrincipal);
//                    _INotyfService.Success("Đăng nhập thành công");
//                    account.LastLogin = DateTime.Now;
//                    _context.Update(account);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction("Index", "AdminHome");

//                    //bool isEmail = Utilities.IsValidEmail(accounts.UserName);
//                    //if (!isEmail)
//                    //{
//                    //    return View(accounts);
//                    //}
//                    //var customer = _context.Customers
//                    //        .SingleOrDefault(x => x.Email.ToLower().Trim() == accounts.UserName.ToLower().Trim());
//                    //if (customer == null)
//                    //{

//                    //    _INotyfService.Warning("Thông tin tài khoàn chưa tồn tài");
//                    //    return View(accounts);
//                    //}

//                    //string pass = (accounts.Password + customer.Salt.Trim()).ToMD5();
//                    //if (customer.Password != pass)
//                    //{
//                    //    _INotyfService.Error("Thông tin đăng nhập chưa chính xác");
//                    //    return View(accounts);
//                    //}
//                    //if (customer.Active == false)
//                    //{
//                    //    return RedirectToAction("ThongBao", "Accounts");
//                    //}

//                    //// Lưu session MaKH
//                    //HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
//                    //var customerID = HttpContext.Session.GetString("CustomerId");
//                    //// Indentity

//                    //var claims = new List<Claim>
//                    //    {
//                    //        new Claim(ClaimTypes.Name, customer.FullName),
//                    //        new Claim("CustomerId", customer.CustomerId.ToString()),
//                    //    };
//                    //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
//                    //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//                    //await HttpContext.SignInAsync(claimsPrincipal);
//                    //_INotyfService.Success("Đăng nhập thành công");
//                    //if (string.IsNullOrEmpty(returnUrl))
//                    //{

//                    //    customer.LastLogin = DateTime.Now;
//                    //    _context.Update(customer);
//                    //    await _context.SaveChangesAsync();
//                    //    return Redirect("my-account.html");
//                    //}
//                    //else
//                    //{
//                    //    customer.LastLogin = DateTime.Now;
//                    //    _context.Update(customer);
//                    //    await _context.SaveChangesAsync();
//                    //    return Redirect(returnUrl);
//                    //}


//                }
//            }
//            catch (Exception ex)
//            {

//                //return RedirectToAction("DangKyTaiKhoan", "Accounts");
//            }
//            return View(accounts);
//        }
//    }
//}
