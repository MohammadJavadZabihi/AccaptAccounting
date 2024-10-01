using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IGetBanckAccountServies
    {
        Task<IEnumerable<BankT?>> GetAllBankAccount(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "");
    }
}
