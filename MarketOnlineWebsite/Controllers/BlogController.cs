using Microsoft.AspNetCore.Mvc;

namespace MarketOnlineWebsite.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
