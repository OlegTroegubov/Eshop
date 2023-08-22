using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.Persistence.Configuration;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder
            .HasKey(pc => pc.Id);

        builder
            .Property(pc => pc.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(pc => pc.IsLastInHierarchy)
            .IsRequired();
    }
}