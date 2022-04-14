using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.ModelViews
{
    public class ListCategoriesVM
    {
        public string SelectedOption { get; set; }
        public IEnumerable<Category> ListCategory { get; set; }
    }
}
