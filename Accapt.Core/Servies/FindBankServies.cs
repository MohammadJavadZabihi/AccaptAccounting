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
    public class FindBankServies : IFindBankServies
    {
        private readonly AccaptFContext _context;
        public FindBankServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<BankT?> GetBankAccountByName(string bankName, string userId)
        {
            return await _context.Banks.FirstOrDefaultAsync(b => b.BankName == bankName && b.Id == userId);
        }

        public async Task<bool> IsExistBankAccount(string bankBranch, string bankName, string userId)
        {
            return await _context.Banks.AnyAsync(b => b.BankName == bankName && b.BankBranch == bankBranch
                                                   && b.Id == userId);
        }
    }
}
