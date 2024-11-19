    using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using AccaptFullyVersion.Core.Generator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class PepoleServies : IPepoleServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindPepolServies _findPepolServies;
        private readonly UserManager<IdentityUser> _userManager;
        public PepoleServies(AccaptFContext context,
            IFindPepolServies findPepolServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));

        }

        public async Task<AddPepolDTO?> AddPepole(AddPepolDTO pepole, string userId)
        {
            try
            {
                if (pepole == null)
                    throw new ArgumentNullException();

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    throw new Exception("Null User");

                var isExistPepo = await _findPepolServies.GetPepoleByName(pepole.PepoName, pepole.PepoCode, userId);

                if (isExistPepo != null)
                {
                    return null;
                }

                var generateId = NameGenerator.GenerateUniqCode();

                var pepo = await _findPepolServies.IsExixstPepoleById(generateId, userId);

                while (pepo)
                {
                    generateId = NameGenerator.GenerateUniqCode();

                    pepo = await _findPepolServies.IsExixstPepoleById(generateId, userId);
                }

                Pepole addPepo = new Pepole
                {
                    PepoId = generateId,
                    Address = pepole.Address,
                    Id = userId,
                    PepoName = pepole.PepoName,
                    PepoType = pepole.PepoType,
                    PhoneNumber = pepole.PhoneNumber,
                    PepoCode = pepole.PepoCode
                };

                _context.People.Add(addPepo);
                await _context.SaveChangesAsync();

                return pepole;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        public async Task<bool> DeletPepole(Pepole pepole)
        {
            try
            {
                if (pepole == null)
                    throw new ArgumentNullException();

                _context.People.Remove(pepole);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        public async Task<bool> DeletPepole(string pepoId, string userId)
        {
            if (string.IsNullOrEmpty(pepoId) || string.IsNullOrEmpty(userId))
                throw new ArgumentNullException();

            try
            {
                var findPeop = await _findPepolServies.GetPepoleById(pepoId, userId);

                if (findPeop == null)
                    throw new ArgumentNullException();

                _context.Remove(findPeop);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        public async Task<bool> DeletPepoleByName(string pepoName, string pepolCode, string userId)
        {
            if (string.IsNullOrEmpty(pepoName) || string.IsNullOrEmpty(userId))
                throw new ArgumentNullException();

            try
            {
                var findPeop = await _findPepolServies.GetPepoleByName(pepoName, pepolCode, userId);

                if (findPeop == null)
                    throw new ArgumentNullException();

                _context.Remove(findPeop);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }

        public async Task<IEnumerable<Pepole?>> GetPepole(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException();

            var userExixst = await _userManager.FindByIdAsync(userId);

            if (userExixst == null)
                throw new ArgumentNullException();

            if (pageSize == 0)
                pageSize = 8;

            IQueryable<Pepole> result = _context.People.AsNoTracking();

            result = result.Where(p => p.Id == userId);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.PepoName.Contains(filter) || p.PhoneNumber.Contains(filter) ||
                                        p.Address.Contains(filter) ||
                                        p.PepoType.Contains(filter) || p.PepoCode.Contains(filter));

                if (result.Count() <= 0)
                    return null;

                return result;
            }

            var skip = (pageNumber - 1) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<bool> UpdatePepole(UpdatePepoleDTO updatePepoleDTO, string pepolName,
            string pepolCode, string userId)
        {
            try
            {
                if (updatePepoleDTO == null)
                    throw new ArgumentNullException();

                var pepoExist = await _findPepolServies.GetPepoleByName(pepolName, pepolCode,userId);

                if (pepoExist == null)
                    return false;

                pepoExist.PepoType = updatePepoleDTO.PepoType;
                pepoExist.Address = updatePepoleDTO.Address;
                pepoExist.PhoneNumber = updatePepoleDTO.PhoneNumber;
                pepoExist.PepoName = updatePepoleDTO.PepoName;
                pepoExist.PepoCode = updatePepoleDTO.PepoCode;

                _context.People.Update(pepoExist);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex.Message));
            }
        }
    }
}
