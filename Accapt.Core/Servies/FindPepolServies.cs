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
    public class FindPepolServies : IFindPepolServies
    {
        private readonly AccaptFContext _context;
        public FindPepolServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Pepole?> GetPepoleById(string pepoName, string userId)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.PepoName == pepoName && p.Id == userId);
        }

        public async Task<Pepole?> GetPepoleByName(string userName, string pepoleCode, string userId)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.PepoName == userName && 
            p.Id == userId && p.PepoCode == pepoleCode);
        }

        public async Task<bool> IsExistPepole(string pepoName, string userId)
        {
            return await _context.People.AnyAsync(p => p.PepoName == pepoName && p.Id == userId);
        }

        public async Task<bool> IsExixstPepoleById(string pepoId, string userId)
        {
            return await _context.People.AnyAsync(p => p.PepoId == pepoId && p.Id == userId);

        }
    }
}
