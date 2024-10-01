using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IFindPepolServies
    {
        Task<Pepole?> GetPepoleByName(string userName, string userId);
        Task<Pepole?> GetPepoleById(string pepoId, string userId);
        Task<bool> IsExixstPepoleById(string pepoId, string userId);
        Task<bool> IsExistPepole(string pepoName, string userId);
    }
}
