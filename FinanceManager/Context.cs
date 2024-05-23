using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
    }
}
