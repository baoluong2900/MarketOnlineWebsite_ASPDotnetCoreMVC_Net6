using System.ComponentModel.DataAnnotations;

namespace MarketOnlineWebsite.ModelViews
{
    public class PersonalVM
    {
        public int CustomerId { get; set; }

        [Display(Name = "Họ Và Tên")]
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [MaxLength(150)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Email nhập không hợp lệ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(10)]
        [MinLength(10, ErrorMessage = "Số điện thoại phải ít nhất 10 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        public DateTime Birhday { get; set; }
        public string Avatar { get; set; }
    }
}
