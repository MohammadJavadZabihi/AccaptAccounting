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

        public Task<IEnumerable<ProviderServiceListS?>> GetAll(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "")
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int ProviderWorkId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UpdateProviderServiceListDTO updateProviderServiceListDTO)
        {
            throw new NotImplementedException();
        }
    }
}
