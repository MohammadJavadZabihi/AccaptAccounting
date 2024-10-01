using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IFindUserServies
    {
        Task<bool> IsExsistUserName(string userName);
        Task<bool> IsExsistEmail(string email);
        Task<Users?> FindUserByUserName(string userName);
        Task<Users?> FindUserById(string Id);
    }
}
