using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class ProductUpdateDTO
    {
        [DisplayName("نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Productname { get; set; } = string.Empty;

        [DisplayName("قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public decimal Price { get; set; }

        [DisplayName("دسته بندی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CatrgoryId { get; set; }

        [DisplayName("تعداد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductCount { get; set; }

        [DisplayName("توضیحات محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; } = string.Empty;
    }
}
