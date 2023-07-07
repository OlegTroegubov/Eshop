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

        var parentCategories = Enumerable.Range(1, 100)
            .Select(x => new ProductCategory
            {
                Name = $"Категория №{x}",
                ParentProductCategoryId = null
            });
        await _context.AddRangeAsync(parentCategories);
        await _context.SaveChangesAsync();
        var categories = Enumerable.Range(101, 200)
            .Select(x => new ProductCategory
            {
                Name = $"Категория №{x}",
                ParentProductCategoryId = x - 100
            }); 
        await _context.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        
        var products = Enumerable.Range(1, 100)
            .Select(x => new Product
            {
                ProductCategoryId = new Random().Next(1, 200),
                Title = $"Продукт №{x}",
                Price = new Random().Next(100, 3000)
            }).
            ToList();
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }
}