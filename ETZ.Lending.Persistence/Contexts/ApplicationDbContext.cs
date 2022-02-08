using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Configurations;

namespace ETZ.Lending.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        }
        
        public DbSet<ProductEntity> Products { get; set; }
        
        public DbSet<LentProductEntity> LentProducts { get; set; }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(static e => e.Entity is IModificationDateTime && e.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is not IModificationDateTime entity) continue;

                entity.UpdatedAt = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.Now;
                }
            }
        }
    }
}