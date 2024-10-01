using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class UserServies : IUserServies
    {
        private AccaptFContext _context;
        public UserServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
