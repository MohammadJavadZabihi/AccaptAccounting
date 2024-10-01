using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class Invoice
    {
        public Invoice()
        {
            
        }

        [Key]
        public int InvoiceId { get; set; }

        [Required]
        [MaxLength(200)]
        public string InvoiceName { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeOfInvoice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public bool CreditorStatuce { get; set; }

        [Required]
        public DateTime DateOfSubmitInvoice { get; set; }

        [Required]
        public DateTime NextDateVisit { get; set; }

        [MaxLength(800)]
        public string Description { get; set; }


        #region Realtion

        [JsonIgnore]
        public Users Users { get; set; }
        public IEnumerable<InvoiceDetails> InvoiceDetails { get; set; }

        #endregion
    }
}
