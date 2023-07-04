using Eshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Persistence.Configuration;

public class SubProductCategoryConfiguration : IEntityTypeConfiguration<SubProductCategory>
{
    public void Configure(EntityTypeBuilder<SubProductCategory> builder)
    {
        builder
            .HasKey(sbp => sbp.Id);

        builder
            .Property(sbp => sbp.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}