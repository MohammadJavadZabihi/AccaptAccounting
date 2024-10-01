using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accapt.DataLayer.Entities;

namespace Accapt.Core.DTOs
{
    public class AddInvoicesDTO
    {
        public string InvoiceName { get; set; } = string.Empty;

        [Required]
        public bool CreditorStatuce { get; set; }

        public string TypeOfInvoice { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        [Required]
        public string PepolName { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public int TotalDiscount { get; set; }

        [Required]
        public DateTime DateOfSubmitInvoice { get; set; }

        [Required]
        public DateTime NextDateVisit { get; set; }

        [MaxLength(800)]
        public string Description { get; set; }

        public IEnumerable<InvoiceDetailsDTO> InvoiceDetails { get; set; }
    }
}
