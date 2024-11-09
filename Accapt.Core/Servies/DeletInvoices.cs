using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class DeletInvoices : IDeletInvoices
    {
        private readonly AccaptFContext _context;
        private readonly IFindInvoices _findInvoices;
        private readonly IFindPepolServies _findPepolServies;
        private readonly UserManager<IdentityUser> _userManager;
        public DeletInvoices(AccaptFContext context,
            IFindInvoices findInvoices,
            IFindPepolServies findPepolServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findInvoices = findInvoices ?? throw new ArgumentException(nameof(findInvoices));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        public async Task<bool> DeletInvoice(int invoiceId, string userId)
        {
            if(invoiceId <= 0)
            {
                throw new ArgumentException();
            }

            var invoiceExist = await _findInvoices.FindInvoiceById(invoiceId, userId);

            if(invoiceExist == null)
            {
                throw new ArgumentNullException();
            }

            var pepoName = invoiceExist.InvoiceName;
            var totalPrice = invoiceExist.TotalPrice;
            var amountPaid = invoiceExist.AmountPaid;

            var invDetalies = _context.InvoiceDetails.Where(d => d.InvoiceId == invoiceExist.InvoiceId && d.Id == userId);

            foreach(var item in invDetalies)
            {
                _context.InvoiceDetails.Remove(item);
            }
            await _context.SaveChangesAsync();

            _context.Invoices.Remove(invoiceExist);

            //var pepoExist = await _findPepolServies.GetPepoleByName(pepoName, userId);

            //if(pepoExist != null)
            //{
            //    pepoExist.PriceOfDebtorCreditor -= (totalPrice - amountPaid);

            //    if (pepoExist.PriceOfDebtorCreditor > 0)
            //        pepoExist.DebtorCreditor = "بدهکار";
            //    else if (pepoExist.PriceOfDebtorCreditor < 0)
            //        pepoExist.DebtorCreditor = "هیچکدام";
            //    else
            //        pepoExist.DebtorCreditor = "بستانکار";

            //    _context.People.Update(pepoExist);
            //}

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
