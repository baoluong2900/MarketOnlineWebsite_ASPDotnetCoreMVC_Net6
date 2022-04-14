using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.ModelViews
{
    public class HomeViewVM
    {
        public List<News> lsNews { get; set; }
        public List<ProductHomeVM> Products { get; set; }
        public Advertisement lsAdvertisement { get; set; }
        public Product Product { get; set; }
    }
}


