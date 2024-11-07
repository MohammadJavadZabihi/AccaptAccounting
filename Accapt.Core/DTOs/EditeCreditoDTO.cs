using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class EditeCreditoDTO
    {
        [Required]
        public int CreditorId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Descriptions { get; set; }

        [Required]
        public string Statuce { get; set; }
    }
}
