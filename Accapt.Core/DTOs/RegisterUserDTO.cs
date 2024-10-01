using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "لطفا طول رشته از {0} بیشتر نباشد")]
        [DisplayName("نام")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا نام خوانوادگی را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "لطفا طول رشته از {0} بیشتر نباشد")]
        [DisplayName("نام خوانوادگی")]
        public string Family { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "لطفا طول رشته از {0} بیشتر نباشد")]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "لطفا طول رشته از {0} بیشتر نباشد")]
        [DisplayName("ایمیل")]
        [EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نیست")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد نمایید")]
        [DisplayName("رمز عبور")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا تکرار رمز عبور خود را وارد نمایید")]
        [DisplayName("تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمرز عبور با تکرار رمز عبور مغایرت دارد")]
        public string RePassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا شماره همراه خود را وارد نمایید")]
        [DisplayName("شماره همراه")]
        [MaxLength(200, ErrorMessage = "لطفا طول رشته از {0} بیشتر نباشد")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
