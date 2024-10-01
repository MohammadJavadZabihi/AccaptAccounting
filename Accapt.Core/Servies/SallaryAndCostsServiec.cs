﻿using Accapt.Core.DTOs;
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
    public class SallaryAndCostsServiec : ISallaryAndCostsServiec
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        public SallaryAndCostsServiec(AccaptFContext context,
            IFindUserServies findUserServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
        }
        public async Task<bool> AddNewSallaryAndCosts(AddSallaryAndCostsDTO addSallaryAndCostsDTO)
        {
            var user = await _findUserServies.FindUserById(addSallaryAndCostsDTO.UserId);
            var isExistSallary = await IsExistSallaryWithName(addSallaryAndCostsDTO.SallaryAndCostsName,
                addSallaryAndCostsDTO.UserId);

            if (user != null && isExistSallary == false)
            {
                SallaryAndCosts sallaryAndCosts = new SallaryAndCosts
                {
                    DateOfSubmit = Convert.ToDateTime(addSallaryAndCostsDTO.DateOfSubmitForShow),
                    DateOfSubmitForShow = addSallaryAndCostsDTO.DateOfSubmitForShow,
                    Descriptions = addSallaryAndCostsDTO.Descriptions,
                    PriceOfSallaryAndCosts = addSallaryAndCostsDTO.PriceOfSallaryAndCosts,
                    SallaryAndCostsName = addSallaryAndCostsDTO.SallaryAndCostsName,
                    UserId = user.Id,
                    User = user,
                };

                await _context.SallaryAndCosts.AddAsync(sallaryAndCosts);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeletSallaryAndCosts(int sallaryId, string userId)
        {
            var user = await _findUserServies.FindUserById(userId);

            if (user != null)
            {
                var sallary = await FindSallaryAndCostsById(sallaryId, userId);

                if(sallary != null)
                {
                    _context.SallaryAndCosts.Remove(sallary);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<bool> EditeSallaryAndCosts(EditeSllaryAndCostsDTO editeSallaryAndCostsDTO)
        {
            var sallary = await FindSallaryAndCostsById(editeSallaryAndCostsDTO.SallaryId, 
                editeSallaryAndCostsDTO.UserId);

            if (sallary != null)
            {
                sallary.SallaryAndCostsName = editeSallaryAndCostsDTO.SallaryAndCostsName;
                sallary.PriceOfSallaryAndCosts = editeSallaryAndCostsDTO.PriceOfSallaryAndCosts;
                sallary.Descriptions = editeSallaryAndCostsDTO.Descriptions;
                sallary.DateOfSubmitForShow = editeSallaryAndCostsDTO.DateOfSubmitForShow;
                sallary.DateOfSubmit = Convert.ToDateTime(editeSallaryAndCostsDTO.DateOfSubmitForShow);
                sallary.UserId = editeSallaryAndCostsDTO.UserId;

                _context.SallaryAndCosts.Update(sallary);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<SallaryAndCosts?> FindSallaryAndCostsById(int sallryId, string userId)
        {
            return await _context.SallaryAndCosts.FirstOrDefaultAsync(s => 
            s.SallaryAndCostsId == sallryId && s.UserId == userId);
        }

        public async Task<IEnumerable<SallaryAndCosts?>> GetAllSallaryAndCosts(int pageSize = 0, int pageNumber = 0, string filter = "", string userId = "")
        {
            var user = await _findUserServies.FindUserById(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if(pageSize == 0)
                pageSize = 10;

            IQueryable<SallaryAndCosts> result = _context.SallaryAndCosts.Where(s => s.UserId == userId).AsNoTracking();

            if(!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(s => s.SallaryAndCostsName.Contains(filter) || s.DateOfSubmitForShow.Contains(filter));

                return result;
            }

            var skip = (pageNumber - 1) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<bool> IsExistSallaryWithName(string sallaryName, string userId)
        {
            return await _context.SallaryAndCosts.AnyAsync(s => 
            s.SallaryAndCostsName == sallaryName && s.UserId == userId);
        }
    }
}
