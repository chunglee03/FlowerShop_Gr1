
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace adminFlowerShop_Gr1.ModelViews
{
    public class ChangePasswordViewModel
    {
        [Key]
        public int CustomerID { get; set; }
        [Display(Name ="Mật khẩu hiện tại")]
        public string? PasswordNow { get; set; }
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Mật khẩu tối thiểu 5 kí tự")]
        public string? Password { get; set; }
        [MinLength(5, ErrorMessage = "Mật khẩu mới tối thiểu 5 kí tự")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
        public string? ConfirmPassword { get; set; }
    }
}
