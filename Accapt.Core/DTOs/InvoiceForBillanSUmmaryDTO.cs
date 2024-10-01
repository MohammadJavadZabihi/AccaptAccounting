using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class InvoiceForBillanSUmmaryDTO
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public double Price { get; set; }
        public double AmountPaide { get; set; }
    }
}
