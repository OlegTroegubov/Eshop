using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Client> Clients { get; }
    public DbSet<Order> Orders { get; }
    public DbSet<Product> Products { get; }
    public DbSet<ProductCategory> ProductCategories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}