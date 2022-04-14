using System.ComponentModel.DataAnnotations;

namespace MarketOnlineWebsite.ModelViews
{
    public class PurchaseVM
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ và Tên")]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email không hợp lệ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn Tỉnh/Thành")]
        public int TinhThanh { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Quận/Huyện")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn Quận/Huyện")]
        public int QuanHuyen { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Phường/Xã")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn Phường/Xã")]
        public int PhuongXa { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public int PaymentID { get; set; }
        public string? Note { get; set; }
    }
}
