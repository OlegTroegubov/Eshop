using Eshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Persistence.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .HasKey(c => c.Id);
        builder
            .Property(c => c.Login)
            .HasMaxLength(250)
            .IsRequired();
        builder
            .Property(c => c.Password)
            .HasMaxLength(250)
            .IsRequired();
        builder
            .Property(c => c.SecondName)
            .HasMaxLength(250);
        builder
            .Property(c => c.FirstName)
            .HasMaxLength(250)
            .IsRequired();
        builder
            .Property(c => c.Phone)
            .HasMaxLength(250);
        builder
            .HasMany(c => c.Orders);
    }
}