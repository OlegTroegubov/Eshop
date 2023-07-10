using Eshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(product => product.ProductCategory)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var products = await GetProductsAsync(cancellationToken);
        await IncludeCategories(products);
        
        var sortedProducts = new List<Product>();
        foreach (var product in products)
        {
            if (product.ProductCategoryId == id)
            {
                sortedProducts.Add(product);
            }
            else
            {
                if (IsContainsCategory(product.ProductCategory, id))
                {
                    sortedProducts.Add(product);
                }
            }
        }
        return sortedProducts;
    }

    
    public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(product => product.ProductCategory)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EditAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        _context.Products.Remove(await GetProductByIdAsync(id, cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
    }

    private bool IsContainsCategory(ProductCategory category, int id)
    {
        if (category == null)
        {
            return false;
        }
        
        if (category.Id == id)
        {
            return true;
        }

        return IsContainsCategory(category.ParentProductCategory, id);
    }
    
    private async Task IncludeCategories(List<Product> products)
    {
        foreach (var product in products)
        {
            await IncludeCategories(product.ProductCategory);
        }
    }

    private async Task IncludeCategories(ProductCategory category)
    {
        if (category != null)
        {
            await _context.Entry(category)
                .Reference(c => c.ParentProductCategory)
                .LoadAsync();

            await IncludeCategories(category.ParentProductCategory);
        }
    }
}