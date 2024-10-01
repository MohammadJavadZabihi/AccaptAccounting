using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class EditeSllaryAndCostsDTO
    {
        public int SallaryId { get; set; }
        public string UserId { get; set; }

        [MaxLength(200)]
        [Required]
        public string SallaryAndCostsName { get; set; }

        [Required]
        public double PriceOfSallaryAndCosts { get; set; }

        [Required, MaxLength(200)]
        public string DateOfSubmitForShow { get; set; }

        [MaxLength(1000)]
        public string Descriptions { get; set; }
    }
}
