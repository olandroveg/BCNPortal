using BCNPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BCNPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public virtual DbSet<BcnUserAccount> BcnUserAccount { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<APImapping> APImapping { get; set; }
        public virtual DbSet<NFmapping> NFmapping { get; set; }
        public virtual DbSet<IDinNRF> IDinNRF { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BcnUserAccountConfiguration());
            builder.ApplyConfiguration(new TokenConfiguration());
            builder.ApplyConfiguration(new APImappingConfiguration());
            builder.ApplyConfiguration(new NFmappingConfiguration());
            builder.ApplyConfiguration(new IDinNRFConfiguration());

        }
        public DatabaseFacade GetDatabase()
        {
            return Database;
        }
        public class ApplicationDbContextFctory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                //string connectionString;
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    optionsBuilder.UseMySql("server = localhost; port = 3306; database = BcnPortal; user = root; password = Cardinals25", new MySqlServerVersion(new Version("8.0.30")));
                else
                    optionsBuilder.UseMySql("server = 192.168.0.18; port = 3306; database = BcnPortal; user = root; password = Cardinals25", new MySqlServerVersion(new Version("8.0.30")));

                //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                //    connectionString = Configuration.GetConnectionString("DefaultConnection");
                //else
                //    connectionString = Configuration.GetConnectionString("ProductionConnection");
                //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                return new ApplicationDbContext(optionsBuilder.Options);

            }
        }
    }
}   