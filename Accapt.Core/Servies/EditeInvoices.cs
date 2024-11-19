using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.AspNetCore.Identity;
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
        private readonly IFindInvoices _findInvoices;
        private readonly IFindPepolServies _findPepolServies;
        private readonly UserManager<IdentityUser> _userManager;
        public EditeInvoices(AccaptFContext context,
            IFindInvoices findInvoices,
            IFindPepolServies findPepolServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _findInvoices = findInvoices ?? throw new ArgumentNullException(nameof(findInvoices));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<bool> UpdateInvoice(UpdateInvoiceDTO dto, string userId)
        {
            try
            {
                if (dto.InvoiceId == 0 || string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Null userId adn invoiceId");
                }

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    throw new ArgumentNullException($"{userId} is not a user");

                var inv = await _findInvoices.FindInvoiceById(dto.InvoiceId, userId);

                if (inv == null)
                    throw new ArgumentNullException($"{dto.InvoiceId} is no a invoice");

                inv.InvoiceName = dto.InvoiceName;
                inv.AmountPaid = dto.AmountPaide;

                _context.Update(inv);

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
