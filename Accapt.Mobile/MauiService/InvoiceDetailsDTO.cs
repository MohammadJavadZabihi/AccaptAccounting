using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Mobile.MauiService
{
    public class InvoiceDetailsDTO
    {
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public int ProductTotalPrice { get; set; }
        public int Discount { get; set; } // اگر مورد استفاده است
    }
}
