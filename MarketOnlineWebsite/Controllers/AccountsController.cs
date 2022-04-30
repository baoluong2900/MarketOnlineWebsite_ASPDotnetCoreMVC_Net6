using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
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
        public IActionResult Dashboard(int? page)
        {
            var acountID = HttpContext.Session.GetString("CustomerId");
            if (acountID != null)
            {
               var customer = _context.Customers.AsNoTracking()
                    .SingleOrDefault(x=>x.CustomerId == Convert.ToInt32(acountID.Trim()));
                if (customer != null)
                {
                    var pageNumber = page == null || page < 0 ? 1 : page.Value;
                    var pageSize = 12;
                    var lsOrder = _context.Orders.AsNoTracking()
                        .Include(x => x.TransactStatus)
                        .Where(x => x.CustomerId == customer.CustomerId && x.Deleted == false)
                        .OrderByDescending(x => x.OrderDate).ToList();
                    //.tolist();
                    ViewBag.Order = lsOrder;
                    //ViewBag.PageList = lsOrder;
                    //PagedList<Order> models = new PagedList<Order>(lsOrder, pageNumber, pageSize);
                    //ViewBag.CurrentPage = pageNumber;
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
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login()
        {
            var customerID = HttpContext.Session.GetString("CustomerId");
            if (customerID != null)
            {
                return Redirect("my-account.html");
            }

            // Check nếu giỏ hàng đã có thì trả về giỏ hàng để khách hàng thấy
            //ViewBag.ShoppingCarts = Cart;
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customers, string? returnUrl = null)
        {
            try
            {
                var carts = HttpContext.Session.Get<List<CartItem>>("Cart");
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customers.UserName);
                    if (!isEmail)
                    {
                        return View(customers);
                    }
                    var customer = _context.Customers
                            .SingleOrDefault(x => x.Email.ToLower().Trim() == customers.UserName.ToLower().Trim());
                    if (customer == null)
                    {

                        _INotyfService.Warning("Thông tin tài khoàn chưa tồn tài");
                        return View(customers);
                    }

                    string pass = (customers.Password + customer.Salt.Trim()).ToMD5();
                    if (customer.Password != pass)
                    {
                        _INotyfService.Error("Thông tin đăng nhập chưa chính xác");
                        return View(customers);
                    }
                    if (customer.Active == false)
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
                    if (string.IsNullOrEmpty(returnUrl))
                    {

                        customer.LastLogin = DateTime.Now;
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                        return Redirect("my-account.html");
                    }
                    else
                    {
                        customer.LastLogin = DateTime.Now;
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                        return Redirect(returnUrl);
                    }


                }
            }
            catch (Exception ex)
            {

                //return RedirectToAction("DangKyTaiKhoan", "Accounts");
            }
            return View(customers);
        }

        [HttpGet]
        [Route("dang-xuat.html",Name ="Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM model)
        {
            try
            {
                if (String.IsNullOrEmpty(model.PasswordNow))
                {
                    ModelState.AddModelError("Password", "Vui lòng nhập mật khẩu");
                }
                var accountID = HttpContext.Session.GetString("CustomerId");
                if(accountID == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                if (ModelState.IsValid)
                {
                    var account = _context.Customers.Find(int.Parse(accountID));
                    if (account == null)
                    {
                        return RedirectToAction("Login", "Accounts");
                    }
                    var pass = (model.PasswordNow.Trim() + account.Salt.Trim()).ToMD5();
                    if (pass == account.Password)
                    {
                        string passNew = (model.Password.Trim() + account.Salt.Trim()).ToMD5();
                        account.Password = passNew;
                        _context.Update(account);
                        _context.SaveChanges();
                        _INotyfService.Success("Cập nhật mật khẩu thành công");
                        return Redirect("my-account.html");
                    }
                }
                else
                {
                    _INotyfService.Error("Thay đổi mật khẩu không thành công");
                    return Redirect("my-account.html");
                    //     return View();

                }

            }
            catch
            {
                _INotyfService.Error("Thay đổi mật khẩu không thành công");
                return Redirect("my-account.html");
                //        return View();
            }
            _INotyfService.Error("Thay đổi mật khẩu không thành công");
            return Redirect("my-account.html");
            //  return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonal([Bind("CustomerId,FullName,Birthday,Avatar,Address,Email,Phone,LocationId,District,Ward,CreateDate,Password,Salt,LastLogin,Active")] Customer customers, Microsoft.AspNetCore.Http.IFormFile fAvatar)
        {
            var accountID = HttpContext.Session.GetString("CustomerId");
            try
            {
                var customer = _context.Customers.AsNoTracking()
                 .SingleOrDefault(x => x.CustomerId == int.Parse(accountID));
                bool checkEmail = _context.Customers.Any(x=> x.Email.Trim().ToLower() == customers.Email.ToString().Trim().ToLower() && x.CustomerId != int.Parse(accountID));
                bool checkPhone = _context.Customers.Any(x => x.Phone.Trim().ToLower() == customers.Phone.ToString().Trim().ToLower() && x.CustomerId != int.Parse(accountID));
                if (checkEmail)
                {
                    _INotyfService.Information("Email đã tồn tại");

                }
                else if(checkPhone)
                {
                    _INotyfService.Information("Số điện thoại đã tồn tại");
                }
                else
                {
                    customer.FullName = customers.FullName.TrimEnd();
                    customer.FullName = Utilities.ToTitleCase(customer.FullName);
                    if (fAvatar != null)
                    {
                        string extension = Path.GetExtension(fAvatar.FileName);
                        string image = Utilities.SEOUrl(customer.FullName) + extension;
                        customer.Avatar = await Utilities.UploadFile(fAvatar, @"customers", image.ToLower());
                    }

                    if (string.IsNullOrEmpty(customer.Avatar)) customer.Avatar = "default.jpg";
                    customer.Email = customers.Email.Trim();
                    customer.Phone= customers.Phone.Trim();
                    _INotyfService.Success("Cập nhật thông tin thành công");
                    _context.Update(customer);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _INotyfService.Error("Cập nhật thất bại");
                return RedirectToAction("Login", "Accounts");

            }
            return RedirectToAction("Login", "Accounts");

        }
    }
}
