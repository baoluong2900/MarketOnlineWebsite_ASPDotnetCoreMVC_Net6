using MarketOnlineWebsite.Extension;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace MarketOnlineWebsite.Controllers.Component
{
    public class NumberCartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            return View(cart);
        }
    }
}
