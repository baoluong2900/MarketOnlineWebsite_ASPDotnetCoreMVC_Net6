using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.Areas.Admin.ModelViews
{
    public class AdminHomeAllVM
    {
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }
}
