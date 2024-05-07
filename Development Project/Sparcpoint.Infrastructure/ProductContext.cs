using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sparcpoint.Models;
using Attribute = Sparcpoint.Models.Attribute;

namespace Sparcpoint.Infrastructure
{
    public class ProductContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attribute> Attributes { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified)
                .Select(x => x.Entity)
                .ToList();

            foreach (var entity in entities)
            {
                if (entity is IHasTimestamp timestamp)
                {
                    timestamp.CreatedTimestamp = DateTime.UtcNow; //injecting ISystemClock omitted for brevity
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}