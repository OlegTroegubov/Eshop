using Eshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Persistence.Configuration;

public class ClientOrderConfiguration : IEntityTypeConfiguration<ClientOrder>
{
    public void Configure(EntityTypeBuilder<ClientOrder> builder)
    {
        builder
            .HasKey(co => co.Id);
        builder
            .HasOne(co => co.Client)
            .WithOne()
            .HasForeignKey<ClientOrder>(co => co.ClientId);
        builder
            .HasOne(co => co.Order)
            .WithOne()
            .HasForeignKey<ClientOrder>(co => co.OrderId);
    }
}