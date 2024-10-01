using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddInvoiceReturnApiDTO
    {
        public bool Statuce { get; set; }
        public AddInvoicesDTO addInvoicesDTO { get; set; }
    }
}
