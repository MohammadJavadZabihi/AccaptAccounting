﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class UpdateInvoiceDTO
    {
        public int InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public decimal AmountPaide { get; set; }
    }
}
