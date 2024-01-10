using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace adminFlowerShop_Gr1.ModelViews
{
    public class RegisterVM
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Ho ten")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [MaxLength(150) ]
        [DataType(DataType.EmailAddress)]
        [Remote(action:"ValidateEmail",controller:"Accounts")]
        public string? Email { get; set; }
        [MaxLength(11)]
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        [Display(Name ="Dien thoai")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action:"ValidatePhone",controller:"Accounts")]
        public string? Phone { get; set; }
        [Display(Name = "Mat khau")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Mật khẩu tối thiểu 5 kí tự")]
        public string? Password { get; set; }
        [MinLength(5, ErrorMessage = "Mật khẩu tối thiểu 5 kí tự")]
        [Display(Name ="Nhập lại mật khẩu")]
        [Compare("Password",ErrorMessage ="Mật khẩu không giống nhau")]
        public string? ComfirmPassword { get; set; }

    }
}
