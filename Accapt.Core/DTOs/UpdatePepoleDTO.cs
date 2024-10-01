using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class UpdatePepoleDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="لطفا نام را وارد کنید")]
        [MaxLength(200)]
        public string PepoName { get; set; }

        [Required(ErrorMessage = "لطفا کد اشتراک را وارد کنید")]
        [MaxLength(200)]
        public string PepoCode { get; set; }

        [Required(ErrorMessage = "لطفا شماره تلفن را وارد کنید")]
        [MaxLength(250)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
        [MaxLength(1000)]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا نوع شخص را مشخص کنید")]
        [MaxLength(50)]
        public string PepoType { get; set; }
    }
}
