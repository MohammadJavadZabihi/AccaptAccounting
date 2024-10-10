using Accapt.Core.DTOs;
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
    internal class ProviderServiceListS : IProviderServiceList
    {
        private readonly IFindUserServies _findUserServies;
        private readonly AccaptFContext _context;
        public ProviderServiceListS(IFindUserServies findUserServies,
            AccaptFContext context)
        {
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<bool> Add(AddProviderServiceListDTO addProviderServiceListDTO)
        {
            var user = await _findUserServies.FindUserById(addProviderServiceListDTO.UserId);

            if(user == null)
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
                TotalAmount = addProviderServiceListDTO.TotalAmount
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
            };

            await _context.VisibleServices.AddAsync(visibleService);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProviderServiceList?>> GetAll(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "")
        {
            var user = await _findUserServies.FindUserById(userId);

            if (user == null)
                return null;

            if (pageSize <= 0)
                pageSize = 8;

            IQueryable<ProviderServiceList?> result = _context.ProviderServiceLists.AsNoTracking();
            result = result.Where(p => p.Id == userId);

            if(!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.CustomerName == filter || p.Address == filter || 
                p.DateOfServiceForShow == filter || p.Descriptions == filter);

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

        public async Task<bool> Remove(int ProviderWorkId, string userId)
        {
            var user = await _findUserServies.FindUserById(userId);

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
            var user = await _findUserServies.FindUserById(updateProviderServiceListDTO.UserId);

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

            return true;
        }
    }
}
