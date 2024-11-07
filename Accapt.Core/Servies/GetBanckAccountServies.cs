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
    public class GetBanckAccountServies : IGetBanckAccountServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly UserManager<IdentityUser> _userManager;
        public GetBanckAccountServies(AccaptFContext context, 
            IFindUserServies findUserServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<IEnumerable<BankT?>> GetAllBankAccount(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "")
        {
            if(string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("Null Exception");

            if (pageSize == 0)
                pageSize = 8;

            IQueryable<BankT> result = _context.Banks.AsNoTracking();

            result = result.Where(b => b.Id == userId);

            if(!string.IsNullOrEmpty(filter))
            {
                result = result.Where(b => b.BankBranch.Contains(filter) || b.BankName.Contains(filter)
                || b.BankNumber.Contains(filter));

                return result;
            }

            var skip = (pageNumber - 1) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();
        }
    }
}
