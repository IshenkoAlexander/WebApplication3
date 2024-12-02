using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Model
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string useConnection = configuration.GetSection("UseDbConnection").Value ?? "DefaultConnection";
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(useConnection));
        }
    }

}
