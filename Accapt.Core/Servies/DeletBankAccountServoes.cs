using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class BankAccountServies : IDeletBankAccount
    {
        private readonly AccaptFContext _conntext;
        private readonly IFindUserServies _finduserServies;
        private readonly UserManager<IdentityUser> _userManager;
        public BankAccountServies(AccaptFContext context,
            IFindUserServies findUserServies,
            UserManager<IdentityUser> userManager)
        {
            _conntext = context ?? throw new ArgumentNullException(nameof(context));
            _finduserServies = findUserServies ?? throw new ArgumentNullException(nameof(findUserServies));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<bool> DeletBankAccount(int bankId, string userId)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userId);

                if (user == null)
                    return false;

                var bankAccount = await _conntext.Banks.FirstOrDefaultAsync(b => b.BankId == bankId && b.Id == userId);

                if (bankAccount == null)
                    return false;

                _conntext.Banks.Remove(bankAccount);
                await _conntext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
