using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using AccaptFullyVersion.Core.Secutiry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class LoginUserServies : ILoginUserServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        public LoginUserServies(AccaptFContext context,
            IFindUserServies findUserServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
        }

        public async Task<ReturniStatuceDTO> LoginUser(LoginUserDTO user)
        {
            try
            {
                if(user == null)
                    return new ReturniStatuceDTO
                    {
                        Data = null,
                        ISuucess = false,
                        Message = "Null Exeption"
                    };

                var existUser = await _findUserServies.FindUserByUserName(user.UserName);

                if (existUser == null)
                    return new ReturniStatuceDTO
                    {   
                        Data = null,
                        ISuucess = false,
                        Message = "Cannot Find User With This UserName"
                    };

                var hashPass = PasswordHelper.EncodePasswordMd5(user.Password);

                var loginUser = await _context.Users.AnyAsync(u => u.UserName == user.UserName && u.Password == hashPass);

                if (!loginUser)
                    return new ReturniStatuceDTO
                    {
                        Data = null,
                        ISuucess = false,
                        Message = "Password Is Wrong"
                    };

                user.UserId = existUser.Id;

                return new ReturniStatuceDTO
                {
                    Data = loginUser,
                    ISuucess = true,
                    Message = "Sucessfully, Welcome back"
                };

            }
            catch (Exception ex)
            {
                return new ReturniStatuceDTO
                {
                    Data = null,
                    ISuucess = false,
                    Message = "Error Message For User Login : " + ex.Message
                };
            }
        }
    }
}
