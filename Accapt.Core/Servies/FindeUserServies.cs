using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class FindeUserServies : IFindUserServies
    {
        private readonly AccaptFContext _context;
        public FindeUserServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Users?> FindUserById(string Id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task<Users?> FindUserByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<bool> IsExsistEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsExsistUserName(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}
