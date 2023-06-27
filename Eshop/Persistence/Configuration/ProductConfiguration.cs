using Eshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Eshop.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(p => p.Id);
        
        builder
            .Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .Property(p => p.Price)
            .IsRequired();
    }
}