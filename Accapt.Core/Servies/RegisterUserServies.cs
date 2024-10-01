using Accapt.Core.DTOs;
using Accapt.Core.Generator;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using AccaptFullyVersion.Core.Generator;
using AccaptFullyVersion.Core.Secutiry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class RegisterUserServies : IRegisterUserServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        public RegisterUserServies(AccaptFContext context,
               IFindUserServies findUserServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
        }

        public async Task<ReturniStatuceDTO> RegisterUser(RegisterUserDTO newuser)
        {
            try
            {
                if (newuser == null)
                    return new ReturniStatuceDTO()
                    {
                        ISuucess = false,
                        Message = "Null Exeption",
                        Data = null
                    };

                if (await _findUserServies.IsExsistUserName(newuser.UserName))
                    return new ReturniStatuceDTO()
                    {
                        Data = null,
                        ISuucess = false,
                        Message = "This UserName is Exsist"
                    };

                if (await _findUserServies.IsExsistEmail(newuser.UserName))
                    return new ReturniStatuceDTO()
                    {
                        Data = null,
                        ISuucess = false,
                        Message = "This Email is Exsist"
                    };


                var guiId = NameGenerator.GenerateUniqCode();

                bool existGUID = await _context.Users.AnyAsync(u => u.Id == guiId);
                while (existGUID)
                {
                    guiId = NameGenerator.GenerateUniqCode();
                    existGUID = await _context.Users.AnyAsync(u => u.Id == guiId);
                }

                var currentDat = DateTime.UtcNow;
                var user = new Users()
                {
                    Id = guiId,
                    RealFullName = newuser.Name + " " + newuser.Family,
                    UserName = newuser.UserName,
                    Password = PasswordHelper.EncodePasswordMd5(newuser.Password),
                    PhoneNumber = newuser.PhoneNumber,
                    IsActive = false,
                    RegisterDate = currentDat,
                    Role = 0,
                    VerifyCode = CodeGeneratorForTwoFactory.GenerateSecureRandomNumber().ToString(),
                    ExpireAccessDate = currentDat.AddYears(1),
                    Email = newuser.Email
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new ReturniStatuceDTO()
                {
                    ISuucess = true,
                    Message = "SuccessFuuly For Register",
                    Data = user
                };
            }
            catch(Exception ex)
            {
                return new ReturniStatuceDTO()
                {
                    Data = null,
                    ISuucess = false,
                    Message = "Field for Registering User : " + ex.Message,
                };
            }
        }
    }
}
