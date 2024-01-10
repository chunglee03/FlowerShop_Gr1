using System.ComponentModel.DataAnnotations;

namespace adminFlowerShop_Gr1.ModelViews
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Địa chỉ Email")]
        public string? UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        //[MinLength(5, ErrorMessage ="mk toi thieu 5 ki tu")]
        public string? Password { get; set; }
    }
}
