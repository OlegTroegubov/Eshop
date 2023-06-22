using Eshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Persistence.Configuration;

public class DbSeeder
{
    private readonly ApplicationDbContext _context;

    public DbSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        var isEmpty = !await _context.Products.AllAsync(null, default);

        if (isEmpty)
        {
            return;
        }

        var products = Enumerable.Range(1, 100)
            .Select(x => new Product
            {
                Title = $"Product №{x}",
                Price = new Random().Next(100, 3000)
            }).
            ToList();
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }
}