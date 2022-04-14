using System.ComponentModel.DataAnnotations;

namespace MarketOnlineWebsite.ModelViews
{
    public class ChangePasswordVM
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
        [MinLength(5, ErrorMessage = "Mật khẩu hiện tại tối thiểu 5 ký tự")]
        [DataType(DataType.Password)]
        public string PasswordNow { get; set; }


        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu mới tối thiểu 5 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng mật khẩu nhập lại")]
        [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
