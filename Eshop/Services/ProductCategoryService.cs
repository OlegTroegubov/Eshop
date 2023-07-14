using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Services;

public class ProductCategoryService
{
    private readonly ApplicationDbContext _context;
    public ProductCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductCategory>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var allProductCategories = await _context.ProductCategories
            .Include(category => category.ParentProductCategory)
            .ToListAsync(cancellationToken);

        var maxDepth = GetMaxDepth(allProductCategories);
        return allProductCategories
            .Where(category => GetDepth(category) == maxDepth)
            .ToList();
    }

    private int GetMaxDepth(List<ProductCategory> productCategories)
    {
        var maxDepth = 0;
        foreach (var category in productCategories)
        {
            var depth = GetDepth(category);
            if (depth > maxDepth)
            {
                maxDepth = depth;
            }
        }

        return maxDepth;
    }
    
    private int GetDepth(ProductCategory category)
    {
        if (category.ParentProductCategory == null)
        {
            return 0;
        }
        return 1 + GetDepth(category.ParentProductCategory);
    }
}