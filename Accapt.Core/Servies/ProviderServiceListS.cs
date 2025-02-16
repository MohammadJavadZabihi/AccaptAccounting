﻿using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class ProviderServiceListS : IProviderServiceListS
    {
        private readonly AccaptFContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ProviderServiceListS(AccaptFContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        public async Task<bool> Add(AddProviderServiceListDTO addProviderServiceListDTO)
        {
            var user = await _userManager.FindByIdAsync(addProviderServiceListDTO.UserId);

            if(user == null)
                return false;

            var provider = await _context.ServiceProviders.FirstOrDefaultAsync(p => p.ProviderName == addProviderServiceListDTO.ProviderName);

            if(provider == null)
                return false;

            ProviderServiceList providerServiceList = new ProviderServiceList
            {
                Address = addProviderServiceListDTO.Address,
                CustomerName = addProviderServiceListDTO.CustomerName,
                DateOfService = Convert.ToDateTime(addProviderServiceListDTO.DateOfServiceForShow),
                DateOfServiceForShow = addProviderServiceListDTO.DateOfServiceForShow,
                Descriptions = addProviderServiceListDTO.Descriptions,
                Id = addProviderServiceListDTO.UserId,
                IsDone = addProviderServiceListDTO.IsDone,
                ProviderName = addProviderServiceListDTO.ProviderName,
                TotalAmount = addProviderServiceListDTO.TotalAmount,
                ServiceProvider = provider,
                ServiceProviderId = provider.ServiceProviderId,
            };

            await _context.ProviderServiceLists.AddAsync(providerServiceList);
            await _context.SaveChangesAsync();

            VisibleService visibleService = new VisibleService
            {
                Id = addProviderServiceListDTO.UserId,
                Descriptions = addProviderServiceListDTO.Descriptions,
                Address = addProviderServiceListDTO.Address,
                DateOfService = addProviderServiceListDTO.DateOfServiceForShow,
                PhoneNumber = addProviderServiceListDTO.PhoneNumber,
                SrviceName = addProviderServiceListDTO.CustomerName,
                Statuce = addProviderServiceListDTO.IsDone,
                ProviderWorkId = providerServiceList.ProviderWorkId,
                ProviderName = addProviderServiceListDTO.ProviderName
            };

            await _context.VisibleServices.AddAsync(visibleService);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProviderServiceList?>> GetAll(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "")
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            if (pageSize <= 0)
                pageSize = 8;

            IQueryable<ProviderServiceList?> result = _context.ProviderServiceLists.AsNoTracking();
            result = result.Where(p => p.Id == userId);

            if(!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.CustomerName.Contains(filter) || p.Address.Contains(filter) || 
                p.DateOfServiceForShow.Contains(filter) || p.Descriptions.Contains(filter));

                if(result.Count() < 0)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }

            var skip = (pageNumber - 1 ) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<VisibleService?>> GetAllServiceForMobile(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "", string providerName = "")
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var provideExixst = await _context.ServiceProviders.AnyAsync(p => p.ProviderName == providerName);

            if (!provideExixst)
                return null;

            if(pageSize <= 0) 
                pageSize = 8;

            IQueryable<VisibleService> result = _context.VisibleServices.AsNoTracking();
            result = result.Where(v => v.Id == userId && v.ProviderName == providerName);

            if(!string.IsNullOrEmpty(filter))
            {
                result = result.Where(v => v.Statuce.Contains(filter) || v.PhoneNumber.Contains(filter) || v.SrviceName.Contains(filter) || 
                v.DateOfService.Contains(filter) || v.Descriptions.Contains(filter));

                if(result.Count() > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }

            var skip = (pageNumber - 1) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<bool> Remove(int ProviderWorkId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            var providerList = await _context.ProviderServiceLists.FirstOrDefaultAsync(p => p.ProviderWorkId == ProviderWorkId
             && p.Id == userId);

            if (providerList == null)
                return false;

            _context.ProviderServiceLists.Remove(providerList);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(UpdateProviderServiceListDTO updateProviderServiceListDTO)
        {
            var user = await _userManager.FindByIdAsync(updateProviderServiceListDTO.UserId);

            if (user == null)
                return false;

            var providerList = await _context.ProviderServiceLists.FirstOrDefaultAsync(p => p.CustomerName == updateProviderServiceListDTO.ServiceName
             && p.Id == updateProviderServiceListDTO.UserId);

            if (providerList == null)
                return false;

            providerList.CustomerName = updateProviderServiceListDTO.ServiceName;
            providerList.Address = updateProviderServiceListDTO.Address;
            providerList.TotalAmount = updateProviderServiceListDTO.Amount;
            providerList.IsDone = updateProviderServiceListDTO.IsDone;
            providerList.DateOfServiceForShow = updateProviderServiceListDTO.Date;
            providerList.DateOfService = Convert.ToDateTime(updateProviderServiceListDTO.Date);

            _context.ProviderServiceLists.Update(providerList);
            await _context.SaveChangesAsync();

            var visibleService = await _context.VisibleServices.FirstOrDefaultAsync(v => v.Id == updateProviderServiceListDTO.UserId && v.ProviderWorkId == providerList.ProviderWorkId);

            if (visibleService == null)
                return false;

            _context.VisibleServices.Remove(visibleService);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
