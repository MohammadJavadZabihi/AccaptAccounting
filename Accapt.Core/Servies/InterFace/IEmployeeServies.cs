using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IEmployeeServies
    {
        Task<bool> AddEmployee(AddEmployeeDTO epmloyee, string userId);
        Task<bool> DeletEmployee(DeletEmployeeDTO deletEpmloyee);
        Task<IEnumerable<Epmloyee>> GetEmployeeList(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "");
    }
}
