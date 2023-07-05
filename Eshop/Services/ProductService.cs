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
            .Include(p => p.SubProductCategory)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.SubProductCategory)
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
}