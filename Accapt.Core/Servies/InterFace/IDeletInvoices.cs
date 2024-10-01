using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IDeletInvoices
    {
        Task<bool> DeletInvoice(int invoiceId, string userId);
    }
}
