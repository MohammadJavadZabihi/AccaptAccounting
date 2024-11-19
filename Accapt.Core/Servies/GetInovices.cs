using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class GetInovices : IGetInovices
    {
        private readonly AccaptFContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public GetInovices(AccaptFContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

            public async Task<IEnumerable<Invoice?>> GetAllInvoice(int pageNumber = 1, int pageSize = 0,
                string filter = "", string userId="")
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                if (pageSize == 0)
                    pageSize = 8;

                IQueryable<Invoice> result = _context.Invoices.AsNoTracking();

                result = result.Where(I => I.Id == userId);

                if (!string.IsNullOrEmpty(filter))
                {
                    result = result.Where(I => I.InvoiceName.Contains(filter));

                    if (result.Count() == 0)
                    {
                        throw new ArgumentNullException();
                    }

                    return result;
                }

                var skip = (pageNumber - 1) * pageSize;

                return await result.Skip(skip).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        public async Task<IEnumerable<InvoiceDetails?>> GetInvoiceDetails(string userId, int inoviceId)
        {
            try
            {
                if (userId == null || inoviceId == 0)
                    throw new ArgumentNullException();

                return await _context.InvoiceDetails.Where(I => I.Id == userId && I.InvoiceId == inoviceId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }
    }
}
