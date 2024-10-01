using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IDebtorCreditorsService
    {
        Task<AddDebtorCreditorsDTO?> AddDebtorCreditors(AddDebtorCreditorsDTO addDebtorCreditorsDTO);
        Task<bool> DeletCreditors(CreditorDTO creditorDTO);
        Task<bool> EditeCreditors(EditeCreditoDTO creditorDTO);
        Task<bool> IsPay(CreditorDTO creditorDTO);
        Task<IEnumerable<DebtorCreditor?>> GetAllDebtorCreditors(int pageNumber = 1, int pageSize = 0,
                                             string filter = "", string userId = "");
        Task<DebtorCreditor?> GetSingle(CreditorDTO creditorDTO);
    }
}
