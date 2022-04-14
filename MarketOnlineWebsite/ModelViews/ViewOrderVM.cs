using MarketOnlineWebsite.Models;

namespace MarketOnlineWebsite.ModelViews
{
    public class ViewOrderVM
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public string FullName { get; set; }
        public string Payment { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string TransactStatus { get; set; }
        public string TinhThanh { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public int CheckOrder { get; set; }
    }
}
