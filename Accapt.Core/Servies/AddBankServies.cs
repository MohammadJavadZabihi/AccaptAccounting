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
    public class AddBankServies : IAddBankServies
    {

        private readonly AccaptFContext _context;
        private readonly IFindBankServies _findBankServies;
        private readonly IFindUserServies _findUserServies;
        private readonly UserManager<IdentityUser> _userManager;
        public AddBankServies(AccaptFContext context, 
            IFindBankServies findBankServies,
            IFindUserServies findUserServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findBankServies = findBankServies ?? throw new ArgumentException(nameof(findBankServies));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        public async Task<AddBankDTO?> AddBank(AddBankDTO addBankDTOcs ,string userId)
        {
            if (addBankDTOcs == null)
                throw new ArgumentNullException("null Exeption");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            if (await _findBankServies.IsExistBankAccount(addBankDTOcs.BankBranch, addBankDTOcs.BankName, userId))
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
                    Id = userId,
                };

                await _context.Banks.AddAsync(bank);
                await _context.SaveChangesAsync();

                return addBankDTOcs;
            }
        }
    }
}
