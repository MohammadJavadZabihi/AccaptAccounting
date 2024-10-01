using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class BillanSummaryDTO
    {
        public IEnumerable<InvoiceForBillanSUmmaryDTO?> PlusInvoices { get; set; }
        public IEnumerable<InvoiceForBillanSUmmaryDTO?> NegetiveInvoices { get; set; }
        public IEnumerable<SallryAndCostForBillanSummary?> SallaryAndCosts{ get; set; }
        public double PlusPrice { get; set; }
        public double NegetivePrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
