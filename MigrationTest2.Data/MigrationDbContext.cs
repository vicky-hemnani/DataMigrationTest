using Microsoft.EntityFrameworkCore;
using MigrationTest2.Domain;
using System;

namespace MigrationTest2.Data
{
    public class MigrationDbContext : DbContext
    {
        public DbSet<SourceModel> sourceModels { get; set; }

        public DbSet<DestinationModel> destinationModels { get; set; }

        public DbSet<MigrationModel> migrationModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MigrationDatabase2",opt => opt.MaxBatchSize(100)); ;
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DestinationModel>()
                .HasIndex(b => b.SourceId)
                .IsUnique();
        }
    }
}
