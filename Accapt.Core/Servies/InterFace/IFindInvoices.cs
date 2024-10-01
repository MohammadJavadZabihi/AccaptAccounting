using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IFindInvoices
    {
        Task<Invoice?> FindInvoiceById(int invoicId, string userId);
        Task<InvoiceDetails?> FindInvoiceDetailsById(int invoicId, string userId);
    }
}
