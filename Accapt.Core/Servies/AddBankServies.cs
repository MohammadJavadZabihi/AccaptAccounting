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
    public class AddBankServies : IAddBankServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindBankServies _findBankServies;
        private readonly IFindUserServies _findUserServies;
        public AddBankServies(AccaptFContext context, 
            IFindBankServies findBankServies,
            IFindUserServies findUserServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findBankServies = findBankServies ?? throw new ArgumentException(nameof(findBankServies));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
        }

        public async Task<AddBankDTO?> AddBank(AddBankDTO addBankDTOcs)
        {
            if (addBankDTOcs == null)
                throw new ArgumentNullException("null Exeption");

            var user = await _findUserServies.FindUserById(addBankDTOcs.Id);

            if (user == null)
                return null;

            if (await _findBankServies.IsExistBankAccount(addBankDTOcs.BankBranch, addBankDTOcs.BankName, addBankDTOcs.Id))
            {
                return null;
            }
            else
            {
                BankT bank = new BankT
                {
                    BankAddress = addBankDTOcs.BankAddress,
                    BankBranch = addBankDTOcs.BankBranch,
                    BankName = addBankDTOcs.BankName,
                    BankNumber = addBankDTOcs.BankNumber,
                    HaseCheck = addBankDTOcs.HaseCheck,
                    User = user,
                    Id = addBankDTOcs.Id,
                };

                await _context.Banks.AddAsync(bank);
                await _context.SaveChangesAsync();

                return addBankDTOcs;
            }
        }
    }
}
