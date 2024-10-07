using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IProviderService
    {
        Task<bool> Add(AddServiceProviderDTO addServiceProviderDTO);
        Task<bool> Remove(int providerId, string userId);
        Task<bool> Update(int providerId, string userId, string currentPassword, string newPassword);
        Task<IEnumerable<ServiceProvider?>> GetAll(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "");
    }
}
