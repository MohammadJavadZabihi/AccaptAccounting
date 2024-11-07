using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Accapt.Core.Servies
{
    public class FindChekServies : IFindChekServies
    {
        private readonly AccaptFContext _conetxt;
        private readonly IFindUserServies _findUserServies;
        private readonly UserManager<IdentityUser> _userManager;
        public FindChekServies(AccaptFContext context,
            IFindUserServies findUserServies,
            UserManager<IdentityUser> userManager)
        {
            _conetxt = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }
        public async Task<Chek?> GetChekByCgekNumber(string chekNumber, string userId)
        {
            return await _conetxt.Cheks.FirstOrDefaultAsync(ch => ch.CheckNumber == chekNumber && ch.Id == userId);
        }

        public async Task<IEnumerable<Chek?>> GetCheks(string filter = "", string userId = "", int pageNumber = 1, int pageSize = 0)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if(user == null)
                    throw new ArgumentNullException("Null User Account");

                if(pageSize <= 0)
                    pageSize = 10;

                IQueryable<Chek> result = _conetxt.Cheks.Where(ch => ch.Id == userId).AsNoTracking();

                if (!string.IsNullOrEmpty(filter))
                {
                    result = result.Where(ch => ch.ChekcBankName.Contains(filter) || ch.CheckNumber.Contains(filter)
                    || ch.DueDateSearch.Contains(filter) || ch.CurrentDateSearch.Contains(filter));

                    if (result.Count() == 0)
                    {
                        throw new ArgumentNullException("Null Result For Search Filter");
                    }

                    return result;
                }

                var skip = (pageNumber - 1) * pageSize;

                return await result.Skip(skip).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex.Message));
            }
        }

        public async Task<bool> IsExistChek(string chekNumber, string userId)
        {
            return await _conetxt.Cheks.AnyAsync(ch => ch.CheckNumber == chekNumber && ch.Id == userId);
        }
    }
}
