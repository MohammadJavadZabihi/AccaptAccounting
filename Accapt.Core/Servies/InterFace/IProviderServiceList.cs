using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IProviderServiceList
    {
        Task<bool> Add(AddProviderServiceListDTO addProviderServiceListDTO);
        Task<bool> Remove(int ProviderWorkId, string userId);
        Task<bool> Update(UpdateProviderServiceListDTO updateProviderServiceListDTO);
        Task<IEnumerable<ProviderServiceList?>> GetAll(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "");
    }
}
