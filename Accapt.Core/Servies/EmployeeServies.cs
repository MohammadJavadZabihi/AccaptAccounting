using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class EmployeeServies : IEmployeeServies
    {
        private readonly AccaptFContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeeServies(AccaptFContext context, 
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        public async Task<bool> AddEmployee(AddEmployeeDTO epmloyee, string userId)
        {
            if(epmloyee != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if(user != null)
                {
                    Epmloyee addEmployee = new Epmloyee
                    {
                        DateOfRegister = Convert.ToDateTime(epmloyee.DateOfRegister),
                        DateOfRegisterShow = epmloyee.DateOfRegister,
                        EmployeeRoll = epmloyee.EmployeeRoll,
                        EpmloyeeName = epmloyee.EpmloyeeName,
                        UserId = user.Id,
                    };

                    await _context.Employees.AddAsync(addEmployee);
                    await _context.SaveChangesAsync();

                    EmployeeDeatails employeeDeatails = new EmployeeDeatails
                    {
                        EmployeeDeatailsCount = epmloyee.EmployeeDeatailsCount,
                        EpmloyeeId = addEmployee.EpmloyeeId,
                        UserId = userId,
                        EmployeeDeatailsName = epmloyee.EmployeeDeatailsName,
                        PriceOfEmployeDeatails = epmloyee.PriceOfEmployeDeatails,
                        Epmloyee = addEmployee
                    };

                    await _context.EmployeeDeatails.AddAsync(employeeDeatails);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            return false;

        }

        public async Task<bool> DeletEmployee(DeletEmployeeDTO deletEpmloyee)
        {
            if(deletEpmloyee != null)
            {
                var user = await _userManager.FindByIdAsync(deletEpmloyee.UserId);

                if(user != null )
                {
                    var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id && 
                      e.EpmloyeeName == deletEpmloyee.EmployeeName);

                    if(employee != null )
                    {
                        _context.Employees.Remove(employee);
                        await _context.SaveChangesAsync();

                        return true;
                    }

                    return false;
                }
                return false;
            }
            return false;
        }

        public async Task<IEnumerable<Epmloyee>> GetEmployeeList(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "")
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            if(pageSize == 0)
                pageSize = 10;

            IQueryable<Epmloyee> epmloyees = _context.Employees;

            epmloyees = epmloyees.Where(e => e.UserId == userId);

            if(!string.IsNullOrEmpty(filter))
            {
                epmloyees = epmloyees.Where(e => e.EpmloyeeName.Contains(filter) || e.DateOfRegisterShow.Contains(filter)
                || e.EmployeeRoll.Contains(filter));

                if(epmloyees.Count() <= 0 )
                {
                    return null;
                }

                return epmloyees;
            }

            int skip = (pageNumber - 1) * pageSize;

            return await epmloyees.Skip(skip).Take(pageSize).ToListAsync();
        }
    }
}
