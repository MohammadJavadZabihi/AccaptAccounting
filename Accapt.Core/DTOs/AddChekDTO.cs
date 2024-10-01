using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddChekDTO
    {
        public string UserId { get; set; }

        [Required]
        public string ChekNumber { get; set; }

        [Required]
        public string ChekBankName { get; set; }

        [Required]
        public decimal ChekPrice { get; set; }

        [Required]
        public string Person { get; set; }

        [Required]
        public DateTime CurrentDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; } 
    }
}
