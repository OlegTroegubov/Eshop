using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Infrastructure.Persistence.Configuration;

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

        if (isNotEmpty) return;

        var productCategories = Enumerable.Range(1, 3)
            .Select(x => new ProductCategory
            {
                Name = $"Категория №{x}",
                ParentProductCategoryId = null,
                ChildrenProductCategory = Enumerable.Range(1, 5)
                    .Select(y => new ProductCategory
                    {
                        Name = $"Категория №{x}.{y}",
                        ParentProductCategoryId = x,
                        ChildrenProductCategory = Enumerable.Range(1, 10)
                            .Select(z => new ProductCategory
                            {
                                Name = $"Категория №{x}.{y}.{z}",
                                IsLastInHierarchy = true,
                                ParentProductCategoryId = y
                            }).ToList()
                    }).ToList()
            });
        await _context.AddRangeAsync(productCategories);
        await _context.SaveChangesAsync();

        var products = Enumerable.Range(1, 160)
            .Select(x => new Product
            {
                ProductCategoryId = new Random().Next(19, 168),
                Title = $"Продукт №{x}",
                Price = new Random().Next(100, 3000)
            }).ToList();
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }
}