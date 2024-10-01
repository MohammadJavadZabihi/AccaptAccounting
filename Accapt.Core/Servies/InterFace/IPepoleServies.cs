using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IPepoleServies
    {
        Task<IEnumerable<Pepole?>> GetPepole(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "");
        Task<AddPepolDTO?> AddPepole(AddPepolDTO pepole);
        Task<bool> DeletPepole(Pepole pepole);
        Task<bool> DeletPepole(string pepoId, string userId);
        Task<bool> DeletPepoleByName(string pepoName, string userId);
        Task<bool> UpdatePepole(UpdatePepoleDTO updatePepoleDTO, string pepolName);
    }
}
