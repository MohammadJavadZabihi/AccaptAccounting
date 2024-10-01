using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class FindInvoices : IFindInvoices
    {
        private readonly AccaptFContext _context;
        public FindInvoices(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Invoice?> FindInvoiceById(int invoicId, string userId)
        {
            return await _context.Invoices.FirstOrDefaultAsync(I => I.InvoiceId == invoicId && I.Id == userId);
        }

        public async Task<InvoiceDetails?> FindInvoiceDetailsById(int invoicId, string userId)
        {
            return await _context.InvoiceDetails.FirstOrDefaultAsync(I => I.InvoiceId == invoicId && I.Id == userId);
        }
    }
}
