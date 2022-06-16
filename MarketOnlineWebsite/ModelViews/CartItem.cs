using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.ModelViews
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amout { get; set; }
        public string discount { get; set; }
        public double TotalMoney => amout * product.Price.Value;

        public double TotalMoneyDiscount { get; set; }
    }
}
