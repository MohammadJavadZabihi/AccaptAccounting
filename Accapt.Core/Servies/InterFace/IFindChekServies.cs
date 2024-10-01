using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IFindChekServies
    {
        Task<bool> IsExistChek(string chekNumber, string userId);
        Task<Chek?> GetChekByCgekNumber(string chekNumber, string userId);
        Task<IEnumerable<Chek?>> GetCheks(string filter = "", string userId = "", int pageNumber = 1, int pageSize = 0);
    }
}
