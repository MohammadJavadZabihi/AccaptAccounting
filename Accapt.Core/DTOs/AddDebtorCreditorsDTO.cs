using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddDebtorCreditorsDTO
    {

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; }

        [Required]
        public double PriceOfDebtorCreditor { get; set; }

        [Required]
        [MaxLength(200)]
        public string Statuce { get; set; }

        [Required]
        public DateTime DateOfPurchase { get; set; }

        [Required]
        [MaxLength(200)]
        public string DateOfPurchaseForShow { get; set; }

        [MaxLength(800)]
        public string Desctiptions { get; set; }
    }
}
