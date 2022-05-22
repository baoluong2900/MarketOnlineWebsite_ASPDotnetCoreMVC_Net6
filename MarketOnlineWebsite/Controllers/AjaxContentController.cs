using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MarketOnlineWebsite.Controllers
{
    public class AjaxContentController : Controller
    {
        public IActionResult HeaderCart()
        {
          
            return ViewComponent("HeaderCart");
        }

        public IActionResult NumberCart()
        {
          
            return ViewComponent("NumberCart");
        }
    }
}
