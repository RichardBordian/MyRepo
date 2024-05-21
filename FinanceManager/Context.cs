using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.EnsureCreated();
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
