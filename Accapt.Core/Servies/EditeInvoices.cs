using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class EditeInvoices : IEditeInvoices
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly IFindInvoices _findInvoices;
        private readonly IFindPepolServies _findPepolServies;
        public EditeInvoices(AccaptFContext context,
            IFindUserServies findUserServies,
            IFindInvoices findInvoices,
            IFindPepolServies findPepolServies)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentNullException(nameof(findUserServies));
            _findInvoices = findInvoices ?? throw new ArgumentNullException(nameof(findInvoices));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
        }
        public async Task<bool> UpdateInvoice(UpdateInvoiceDTO dto)
        {
            try
            {
                if (dto.InvoiceId == 0 || string.IsNullOrEmpty(dto.UserId))
                {
                    throw new Exception("Null userId adn invoiceId");
                }

                var user = await _findUserServies.FindUserById(dto.UserId);

                if (user == null)
                    throw new ArgumentNullException($"{dto.UserId} is not a user");

                var inv = await _findInvoices.FindInvoiceById(dto.InvoiceId, dto.UserId);

                if (inv == null)
                    throw new ArgumentNullException($"{dto.InvoiceId} is no a invoice");

                inv.InvoiceName = dto.InvoiceName;
                inv.AmountPaid = dto.AmountPaide;

                _context.Update(inv);


                //var pepo = await _findPepolServies.GetPepoleByName(dto.InvoiceName, dto.UserId);

                //if (pepo != null)
                //{
                //    pepo.PriceOfDebtorCreditor -= dto.AmountPaide;

                //    if (pepo.PriceOfDebtorCreditor > 0)
                //        pepo.DebtorCreditor = "بدهکار";
                //    else if (pepo.PriceOfDebtorCreditor == 0)
                //        pepo.DebtorCreditor = "هیچکدام";
                //    else
                //        pepo.DebtorCreditor = "بستانکار";

                //    _context.People.Update(pepo);
                //}

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }
    }
}
