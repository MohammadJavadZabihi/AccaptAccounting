using Accapt.Core.Convertor;
using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class UpdateChek : IUpdateChek
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly IFindChekServies _findChekServies;
        private readonly UserManager<IdentityUser> _userManager;
        public UpdateChek(AccaptFContext context,
            IFindUserServies findUserServies,
            IFindChekServies findChekServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _findChekServies = findChekServies ?? throw new ArgumentException(nameof(findChekServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));

        }

        public async Task<Chek?> UpdateChekAcc(ChekUpdateDTO chekUpdateDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException("Null User Ex");

            var chek = await _findChekServies.GetChekByCgekNumber(chekUpdateDTO.ChekNumber, userId);

            if (chek == null)
                throw new ArgumentNullException("Null Chek Ex");

            chek.DueDate = chekUpdateDTO.DueDate;
            chek.DueDateSearch = DateConvertor.ConvertToShamsi(chekUpdateDTO.DueDate);
            chek.CurrentDate = chekUpdateDTO.CurrentDate;
            chek.CurrentDateSearch = DateConvertor.ConvertToShamsi(chekUpdateDTO.CurrentDate);
            chek.ChekPrice = chekUpdateDTO.ChekPrice;

            _context.Cheks.Update(chek);

            await _context.SaveChangesAsync();

            return chek;
        }
    }
}
