using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Accapt.DataLayer.ContextIdentoty
{
    public class ContextIdentityFactory : IDesignTimeDbContextFactory<ContextIdentity>
    {
        public ContextIdentity CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ContextIdentity>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityConnectionDB"));

            return new ContextIdentity(optionsBuilder.Options);
        }
    }
}
