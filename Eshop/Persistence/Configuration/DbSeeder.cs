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

        var parentCategories = Enumerable.Range(1, 3)
            .Select(x => new ProductCategory
            {
                Name = $"Категория №{x}",
                ParentProductCategoryId = null
            });
        await _context.AddRangeAsync(parentCategories);
        await _context.SaveChangesAsync();
        var categories = Enumerable.Range(1, 3)
            .SelectMany(parentIndex =>
                Enumerable.Range(1, 5)
                    .Select(childIndex => new ProductCategory
                    {
                        Name = $"Категория №{parentIndex}.{childIndex}",
                        ParentProductCategoryId = parentIndex
                    })
            )
            .ToList();
        await _context.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        var subCategories = Enumerable.Range(1, 3)
            .SelectMany(parentIndex =>
                Enumerable.Range(1, 5)
                    .SelectMany(childIndex =>
                        Enumerable.Range(1, 10)
                            .Select(nestedIndex => new ProductCategory
                            {
                                Name = $"Ккатегория №{parentIndex}.{childIndex}.{nestedIndex}",
                                ParentProductCategoryId = categories[(parentIndex - 1) * 5 + (childIndex - 1)].Id
                            })
                    )
            )
            .ToList();
        await _context.AddRangeAsync(subCategories);
        await _context.SaveChangesAsync();
        var products = Enumerable.Range(1, 160)
            .Select(x => new Product
            {
                ///Добавляем только последние вложенные по иерархии категории товара
                ProductCategoryId = new Random().Next(19, 168),
                Title = $"Продукт №{x}",
                Price = new Random().Next(100, 3000)
            }).
            ToList();
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }
}