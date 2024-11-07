using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class DeletChekServies : IDeletChekServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly IFindChekServies _findChekServies;
        private readonly UserManager<IdentityUser> _userManager;
        public DeletChekServies(AccaptFContext context,
            IFindUserServies findUserServies, 
            IFindChekServies findChekServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _findChekServies = findChekServies ?? throw new ArgumentException(nameof(findChekServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));

        }

        public async Task<bool> DeletChek(string chekNumber, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException("Null User EX");

            var chek = await _findChekServies.GetChekByCgekNumber(chekNumber, userId);

            if (chek == null)
                throw new ArgumentNullException("Null Chek EX");

            _context.Cheks.Remove(chek);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
