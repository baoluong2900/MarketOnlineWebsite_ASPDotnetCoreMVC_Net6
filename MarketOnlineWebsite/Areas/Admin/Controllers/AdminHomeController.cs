using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Areas.Admin.ModelViews;
using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Security.Claims;


namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {

        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }
        public AdminHomeController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }
        public IActionResult Index()
        {
            AdminHomeAllVM model = new AdminHomeAllVM();
            var accountID = HttpContext.Session.GetString("AccountId");
            var lsProducts = _context.Products.AsNoTracking()
                  .Where(x => x.Active == true && x.HomeFlag == true)
                  .OrderByDescending(x => x.DateCreated)
                  .Take(5)
                  .ToList();

            var lsOrders = _context.Orders.AsNoTracking()
                .Where(x => x.TransactStatusId == 14 && x.Deleted == false)
                .OrderByDescending(x => x.OrderDate)
                .Include(x => x.TransactStatus)
                .Include(x => x.Customer)
                .Take(6)
                .ToList();

            var monthNow = DateTime.Now.Month;
            var totalTevenueMonth = _context.Orders.AsNoTracking().Where(x=>x.OrderDate.Value.Month == monthNow).Sum(x => x.TotalMoney);

            var dayNow = DateTime.Now.Day;
            var totalTevenueDay = _context.Orders.AsNoTracking().Where(x => x.OrderDate.Value.Day == dayNow).Sum(x => x.TotalMoney);

            var totalTevenueAll = _context.Orders.AsNoTracking().Sum(x => x.TotalMoney);

            var countOrders = _context.Orders.AsNoTracking().Count();
            var countSuppliers = _context.Suppliers.AsNoTracking().Count();
            var countCustomers= _context.Customers.AsNoTracking().Count();
            var countAccounts= _context.Accounts.AsNoTracking().Count();

            var sumUsers = countAccounts + countCustomers;
  
            ViewBag.CountSuppliers = countSuppliers;
            ViewBag.CountCustomers = countCustomers;
            ViewBag.CountAccounts = countAccounts;
            ViewBag.CountOrders = countOrders;
            ViewBag.TotalTevenueMonth = totalTevenueMonth;
            ViewBag.TotalTevenueDay = totalTevenueDay;
            ViewBag.TotalTevenueAll = totalTevenueAll;
            ViewBag.ListOrder = lsOrders;
            ViewBag.ListProduct=lsProducts;
            ViewBag.SumUsers = sumUsers;


            //var account = _context.Accounts.AsNoTracking()
            //       .SingleOrDefault(x =>x.AccountId  == Convert.ToInt32(accountID.Trim()));
            return View();
        }

        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public IActionResult ValidateEmail(string UserName)
        //{
        //    try
        //    {
        //        var customer = _context.Accounts.AsNoTracking()
        //            .SingleOrDefault(x => x.Email.ToLower() == UserName.Trim().ToLower());
        //        if (customer! == null)
        //        {
        //            return Json(data: "Email : " + UserName + " Đã được sử dụng<br />");

        //        }
        //        return Json(data: true);
        //    }
        //    catch
        //    {
        //        return Json(data: true);
        //    }
        //}

        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ValidateEmail(LoginViewModel accounts)
        //{
        //    try
        //    {
        //        //var customer = _context.Customers.AsNoTracking()
        //        //    .SingleOrDefault(x => x.Email.ToLower() == UserName.Trim().ToLower());
        //        var account = await _userManager.FindByEmailAsync(accounts.UserName);
        //        if (account != null)
        //        {
        //            return Json(data: "Email : " + account + " Đã được sử dụng<br />");

        //        }
        //        else
        //        {
        //            return Json(true);
        //        }

        //    }
        //    catch
        //    {
        //        return Json(data: true);
        //    }



        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ValidateEmail(LoginViewModel accounts)
        //{
        //    try
        //    {
        //        //var customer = _context.Customers.AsNoTracking()
        //        //    .SingleOrDefault(x => x.Email.ToLower() == UserName.Trim().ToLower());
        //        var account = await _userManager.FindByEmailAsync(accounts.UserName);
        //        if (account != null)
        //        {
        //            return Json(data: "Email : " + account + " Đã được sử dụng<br />");

        //        }
        //        else
        //        {
        //            return Json(true);
        //        }

        //    }
        //    catch
        //    {
        //        return Json(data: true);
        //    }
        //}

        [AcceptVerbs("Get", "Post")]
        public JsonResult ValidateEmail(string UserName)
        {
 
            bool isExits = _context.Accounts.AsNoTracking()
                .Any(x => x.Email.ToLower() == UserName.Trim().ToLower());
            if(isExits)
            {
                return Json(data: false);
            }
            else
            {
                return Json(data: true);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/Admin/dang-nhap-user.html", Name = "DangNhapUser")]
        public IActionResult Login()
        
        {
            var accountID = HttpContext.Session.GetString("AccountId");
            if (accountID != null)
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Admin/dang-nhap-user.html", Name = "DangNhapUser")]
        public async Task<IActionResult> Login(LoginViewModel accounts, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var account = _context.Accounts.Include(x=>x.Role)
                            .SingleOrDefault(x => x.Email.ToLower().Trim() == accounts.UserName.ToLower().Trim() && x.Password == accounts.Password);

                    //var supplier = _context.Suppliers.SingleOrDefault(x => x.Email.ToLower().Trim() == accounts.UserName.ToLower().Trim() && x.Password == accounts.Password);
                    if (account == null)
                    {
                        ModelState.AddModelError(string.Empty, "Tài khoản chưa tồn tại");
                        //_INotyfService.Warning("Chưa tồn tại kìa");
                        return View(accounts);
                    }
                    //if(account.Email == accounts.UserName)
                    //{
                    //    var json = JsonConvert.SerializeObject(accounts);
                    //    return Content(json);
                    //}
                    string pass = (accounts.Password + account.Password.Trim());
                    //vEmail = ValidateEmail(accounts.UserName);
                    //if (!isEmail)
                    //{
                    //    return View(accounts);
                    //}
                    if (account.Password != accounts.Password)
                    {
                        ModelState.AddModelError(string.Empty, "Mật khẩu không đúng");
                        return View(accounts);
                    }
                    if(account.Active == false)
                    {
                        ModelState.AddModelError(string.Empty, "Tài khoản chưa tồn tại");
                        return View(accounts);
                    }

                    HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
                    var accountID = HttpContext.Session.GetString("AccountId");
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, account.FullName, ClaimValueTypes.String),
                            new Claim("RoleId",account.Role.RoleName.ToString() ?? String.Empty,ClaimValueTypes.Integer),
                             new Claim("Avatar",account.Avatar ??  String.Empty, ClaimValueTypes.String),
                            new Claim("AccountId", account.AccountId.ToString(),ClaimValueTypes.Integer),
                        };
            
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _INotyfService.Success("Đăng nhập thành công");
                    account.LastLogin = DateTime.Now;
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "AdminHome");

                }
            }
            catch (Exception ex)
            {

                //return RedirectToAction("DangKyTaiKhoan", "Accounts");
            }
            return View(accounts);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Admin/dang-ky-user.html", Name = "DangKyUser")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Admin/dang-ky-user.html", Name = "DangKyUser")]
        public async Task<IActionResult> Register(RegisterVM Accounts)
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
                        return RedirectToAction("DangKy", "Accounts");
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
        [Route("/Admin/dang-xuat-user.html", Name = "DangXuatUser")]
        public IActionResult DangXuatUser()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("AccountId");
            return RedirectToAction("Login", "AdminHome");
        }
    }
}
