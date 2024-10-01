using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IDeletBankAccount
    {
        Task<bool> DeletBankAccount(int bankId, string userId);
    }
}
