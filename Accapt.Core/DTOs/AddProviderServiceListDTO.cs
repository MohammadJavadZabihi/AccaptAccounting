using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddProviderServiceListDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string ProviderName { get; set; }

        [Required]
        [MaxLength(200)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(800)]
        public string Address { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(150)]
        public string IsDone { get; set; }

        [Required]
        [MaxLength(250)]
        public string DateOfServiceForShow { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descriptions { get; set; }
    }
}
