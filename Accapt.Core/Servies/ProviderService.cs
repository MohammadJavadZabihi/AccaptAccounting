using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using AccaptFullyVersion.Core.Secutiry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class ProviderService : IProviderService
    {
        private readonly IFindUserServies _userServies;
        private readonly AccaptFContext _context;
        public ProviderService(IFindUserServies userServies,
            AccaptFContext context)
        {
            _userServies = userServies ?? throw new ArgumentException(nameof(context));
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<bool> Add(AddServiceProviderDTO addServiceProviderDTO)
        {
            var user = await _userServies.FindUserById(addServiceProviderDTO.UserId);

            if (user == null)
                return false;

            ServiceProvider providerService = new ServiceProvider
            {
                AmountOfCreditor = 0,
                Id = addServiceProviderDTO.UserId,
                IsCreditor = "هیچکدام",
                ProviderName = addServiceProviderDTO.ProviderName,
                ProviderPassword = PasswordHelper.EncodePasswordMd5(addServiceProviderDTO.ProviderPassword),
            };

            await _context.ServiceProviders.AddAsync(providerService);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ServiceProvider?>> GetAll(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "")
        {
            var user = await _userServies.FindUserById(userId);

            if (user == null)
                return null;

            if(pageSize <= 0)
                pageSize = 8;

            IQueryable<ServiceProvider?> result = _context.ServiceProviders.AsNoTracking();

            if(!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.ProviderName == filter || p.IsCreditor == filter);
                
                if(result.Count() <= 0)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }

            var skip = (pageNumber - 1) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();

        }

        public async Task<bool> Remove(int providerId, string userId)
        {
            var user = await _userServies.FindUserById(userId);

            if (user == null)
                return false;

            var provider = await _context.ServiceProviders.FirstOrDefaultAsync(p => p.Id == userId && p.ServiceProviderId == providerId);

            if(provider == null)
                return false;

            _context.ServiceProviders.Remove(provider);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(int providerId, string userId, string currentPassword, string newPassword)
        {
            var user = await _userServies.FindUserById(userId);

            if (user == null)
                return false;

            var provider = await _context.ServiceProviders.FirstOrDefaultAsync(p => p.Id == userId && p.ServiceProviderId == providerId);

            if (provider == null)
                return false;

            string hashPassword = PasswordHelper.EncodePasswordMd5(currentPassword);

            if(provider.ProviderPassword != hashPassword)
                return false;

            string newHashPassword = PasswordHelper.EncodePasswordMd5(newPassword);

            provider.ProviderPassword = newHashPassword;

            _context.ServiceProviders.Update(provider);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
