using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IFindBankServies
    {
        Task<bool> IsExistBankAccount(string bankBranch, string bankName, string userId);
        Task<BankT?> GetBankAccountByName(string bankName, string userId);
    }
}
