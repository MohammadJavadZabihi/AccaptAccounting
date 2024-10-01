using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class InvoiceDetails
    {
        public InvoiceDetails()
        {
            
        }

        [Key]
        public int InvoiceDetailsId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string Id { get; set; }

        [Required]
        public int ProductPrice { get; set; }

        [Required]
        public int ProductCount { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public decimal ProductTotalPrice { get; set; }


        #region Realation

        public Users Users { get; set; }
        public Invoice Invoices { get; set; }

        #endregion
    }
}
