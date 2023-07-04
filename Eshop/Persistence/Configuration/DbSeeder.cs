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

    public async Task SeedAsync()
    {
        var isNotEmpty = await _context.Products.AnyAsync();
        
        if (isNotEmpty)
        {
            return;
        }

        var categories = Enumerable.Range(1, 100)
            .Select(x => new ProductCategory
            {
                Name = $"Категория №{x}",
                SubProductСategories = Enumerable.Range(1,3)
                    .Select(y => new SubProductСategory
                    {
                        Name = $"Подкатегория №{y}"
                    }).ToList()
            });
        await _context.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        
        var products = Enumerable.Range(1, 100)
            .Select(x => new Product
            {
                SubProductCategoryId = x,
                Title = $"Продукт №{x}",
                Price = new Random().Next(100, 3000)
            }).
            ToList();
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }
}