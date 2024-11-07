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
    public class AddChekServies : IAddChekServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindBankServies _findBankServies;
        private readonly IFindUserServies _findUserServies;
        private readonly UserManager<IdentityUser> _userManager;
        public AddChekServies(AccaptFContext context,
            IFindBankServies findBankServies,
            IFindUserServies findUserServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findBankServies = findBankServies ?? throw new ArgumentException(nameof(findBankServies));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }
        public async Task<SingleChekDTO?> AddChek(AddChekDTO addChekDTO, string userId)
        {
            try
            {
                if (addChekDTO != null)
                {
                    var bank = await _findBankServies.GetBankAccountByName(addChekDTO.ChekBankName, userId);

                    var user = await _userManager.FindByIdAsync(userId);

                    if (user == null)
                    {
                        throw new ArgumentNullException("Null User Account");
                    }

                    if (bank == null)
                    {
                        throw new ArgumentNullException("Null Bank Account");
                    }
                    else
                    {
                        Chek chek = new Chek
                        {
                            Bank = bank,
                            CheckNumber = addChekDTO.ChekNumber,
                            ChekcBankName = addChekDTO.ChekBankName,
                            ChekPrice = addChekDTO.ChekPrice,
                            CurrentDate = addChekDTO.CurrentDate,
                            DueDate = addChekDTO.DueDate,
                            Id = userId,
                            CurrentDateSearch = addChekDTO.CurrentDate.ToShortDateString(),
                            DueDateSearch = addChekDTO.DueDate.ToShortDateString(),
                            StatuceOfPaid = "در حال پردازش",
                            TypeOfChek = addChekDTO.Person
                        };

                        await _context.Cheks.AddAsync(chek);

                        await _context.SaveChangesAsync();

                        bank.HaseCheck = true;
                        _context.Update(bank);
                        
                        await _context.SaveChangesAsync();

                        SingleChekDTO addChek = new SingleChekDTO
                        {
                            CheckId = chek.CheckId,
                            CheckNumber = chek.CheckNumber,
                            ChekcBankName = chek.ChekcBankName,
                            ChekPrice = chek.ChekPrice,
                            CurrentDateSearch = chek.CurrentDateSearch,
                            DueDateSearch = chek.DueDateSearch,
                            StatuceOfPaid = chek.StatuceOfPaid,
                            TypeOfChek = chek.TypeOfChek
                        };

                        return addChek;
                    }
                }
                else
                {
                    throw new ArgumentNullException("Null Data Transfer Object");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex.Message));
            }
        }
    }
}
