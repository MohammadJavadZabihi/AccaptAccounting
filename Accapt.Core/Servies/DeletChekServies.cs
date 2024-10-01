using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
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
        public DeletChekServies(AccaptFContext context,
            IFindUserServies findUserServies, IFindChekServies findChekServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _findChekServies = findChekServies ?? throw new ArgumentException(nameof(findChekServies));

        }

        public async Task<bool> DeletChek(string chekNumber, string userId)
        {
            var user = await _findUserServies.FindUserById(userId);

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
