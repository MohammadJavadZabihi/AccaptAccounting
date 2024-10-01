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
    public class DeletBillanServies : IDeletBillanServies
    {
        private readonly AccaptFContext _context;
        public async Task<bool> DeletBillanHistory(Billan billan)
        {
            if (billan == null) 
                throw new ArgumentNullException();

            _context.Billans.Remove(billan);
            await _context.SaveChangesAsync();

            return false;
        }
    }
}
