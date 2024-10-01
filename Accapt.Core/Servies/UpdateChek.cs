using Accapt.Core.Convertor;
using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
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
        public UpdateChek(AccaptFContext context,
            IFindUserServies findUserServies,
            IFindChekServies findChekServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _findChekServies = findChekServies ?? throw new ArgumentException(nameof(findChekServies));

        }

        public async Task<Chek?> UpdateChekAcc(ChekUpdateDTO chekUpdateDTO)
        {
            var user = await _findUserServies.FindUserById(chekUpdateDTO.UserId);

            if (user == null)
                throw new ArgumentNullException("Null User Ex");

            var chek = await _findChekServies.GetChekByCgekNumber(chekUpdateDTO.ChekNumber, chekUpdateDTO.UserId);

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
