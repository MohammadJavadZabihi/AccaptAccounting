using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface ISallaryAndCostsServiec
    {
        Task<bool> AddNewSallaryAndCosts(AddSallaryAndCostsDTO addSallaryAndCostsDTO);
        Task<bool> EditeSallaryAndCosts(EditeSllaryAndCostsDTO editeSallaryAndCostsDTO);
        Task<bool> DeletSallaryAndCosts(int sallaryId, string userId);
        Task<SallaryAndCosts?> FindSallaryAndCostsById(int sallryId, string userId);
        Task<bool> IsExistSallaryWithName(string sallaryName, string userId);
        Task<IEnumerable<SallaryAndCosts?>> GetAllSallaryAndCosts(int pageSize = 0, int pageNumber = 0, 
            string filter = "", string userId = "");
    }
}
