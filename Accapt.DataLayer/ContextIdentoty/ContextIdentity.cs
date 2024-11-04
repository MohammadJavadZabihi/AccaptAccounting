using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Accapt.DataLayer.ContextIdentoty
{
    public class ContextIdentity : IdentityDbContext
    {
        public ContextIdentity(DbContextOptions<ContextIdentity> options) : base(options)
        {
            
        }
        public ContextIdentity()
        {
            
        }
    }
}
