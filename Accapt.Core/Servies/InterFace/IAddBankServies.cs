using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IAddBankServies
    {
        Task<AddBankDTO?> AddBank(AddBankDTO addBankDTOcs);
    }
}
