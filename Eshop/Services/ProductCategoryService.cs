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

    public async Task<List<ProductCategory>> GetCategoriesAsync()
    {
        return await _context.ProductCategories.ToListAsync();
    }
}