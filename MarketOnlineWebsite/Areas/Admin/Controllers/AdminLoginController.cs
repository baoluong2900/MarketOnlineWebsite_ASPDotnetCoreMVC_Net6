using Microsoft.AspNetCore.Mvc;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        public IActionResult DangNhap()
        {
            return View();
        }

        public IActionResult DangKy()
        {
            return View();
        }
    }
}
