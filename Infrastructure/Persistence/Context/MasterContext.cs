using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class MasterContext : DbContext
    {
        #region Constructors
        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {
        }

        #endregion

        #region DbSets
        public DbSet<Product> Products { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQL_URI"));
            }
        }

    }
}
