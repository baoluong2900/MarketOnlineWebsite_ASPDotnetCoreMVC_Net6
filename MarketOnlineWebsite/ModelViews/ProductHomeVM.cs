using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }

        public Product product { get; set; }
        public List<Product> lsProducts { get; set; }
    }
}
