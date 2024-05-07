using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparcpoint.Models;

namespace Sparcpoint.Infrastructure.EntityConfigurations;

public class ProductEntityTypeConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("Products");

        entity.HasMany(x => x.Categories)
            .WithMany(x => x.Products)
            .UsingEntity("ProductCategories");

        entity.HasMany(x => x.Attributes)
            .WithMany(x => x.Products)
            .UsingEntity("ProductAttributes");
    }
}