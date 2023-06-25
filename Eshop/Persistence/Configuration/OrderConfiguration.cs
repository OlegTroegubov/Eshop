using Eshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasKey(o => o.Id);
        
        builder
            .HasOne(o => o.Client)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .Property(o => o.DateOfOrderTime)
            .IsRequired();
        
        builder
            .Property(o => o.Amount)
            .IsRequired();
    }
}