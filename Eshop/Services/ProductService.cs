using Eshop.Dtos.Mappers;
using Eshop.Dtos.Product;
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

    /// <summary>
    ///     Получает список всех продуктов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список всех продуктов.</returns>
    public async Task<List<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(product => product.ProductCategory)
            .Select(x => ProductMapper.MapToDto(x))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    ///     Получает продукт по указанному идентификатору.
    /// </summary>
    /// <param name="productId">Идентификатор продукта.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель продукта Dto с указанным идентификатором.</returns>
    public async Task<ProductDto> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
    {
        return ProductMapper.MapToDto(await _context.Products
            .Include(product => product.ProductCategory)
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken));
    }

    /// <summary>
    ///     Добавляет продукт.
    /// </summary>
    /// <param name="product">Продукт.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель продукта Dto добавленного продукта</returns>
    public async Task<ProductDto> AddAsync(ProductDto product, CancellationToken cancellationToken)
    {
        var productToAdd = ProductMapper.MapToProduct(product);
        await _context.Products.AddAsync(productToAdd, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return ProductMapper.MapToDto(productToAdd);
    }

    /// <summary>
    ///     Изменяет продукт.
    /// </summary>
    /// <param name="product">Продукт.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель продукта Dto добавленного продукта</returns>
    public async Task EditAsync(ProductDto product, CancellationToken cancellationToken)
    {
        _context.Products.Update(ProductMapper.MapToProduct(product));
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    ///     Удаляет продукт по указанному идентификатору.
    /// </summary>
    /// <param name="productId">Идентификатор удаляемоего продукта.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    public async Task DeleteAsync(int productId, CancellationToken cancellationToken)
    {
        _context.Products.Remove(
            await _context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    ///     Возвращает сортированные продукты.
    /// </summary>
    /// <param name="propertyName">Имя параметра для сортировки(свойство продукта).</param>
    /// <param name="sortOrder">Значение сортировки(asc или desc).</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов</returns>
    public async Task<List<ProductDto>> GetSortedProducts(string propertyName, string sortOrder,
        CancellationToken cancellationToken)
    {
        switch (propertyName.ToLower())
        {
            case "price":
                if (sortOrder == "desc") return await GetProductByPriceDesc(cancellationToken);
                return await GetProductByPriceAsc(cancellationToken);

            case "title":
                if (sortOrder == "desc") return await GetProductByTitleDesc(cancellationToken);
                return await GetProductByTitleAsc(cancellationToken);

            default: return await GetProductsAsync(cancellationToken);
        }
    }

    /// <summary>
    ///     Возвращает сортированные продукты по увеличению по имени.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов по имени</returns>
    private async Task<List<ProductDto>> GetProductByTitleAsc(CancellationToken cancellationToken)
    {
        return await _context.Products.Include(product => product.ProductCategory)
            .OrderBy(product => product.Title)
            .Select(product => ProductMapper.MapToDto(product))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    ///     Возвращает сортированные продукты по уменьшению по имени.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов по имени</returns>
    private async Task<List<ProductDto>> GetProductByTitleDesc(CancellationToken cancellationToken)
    {
        return await _context.Products.Include(product => product.ProductCategory)
            .OrderByDescending(product => product.Title)
            .Select(product => ProductMapper.MapToDto(product))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    ///     Возвращает сортированные продукты по увеличению цены.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов по цене</returns>
    private async Task<List<ProductDto>> GetProductByPriceAsc(CancellationToken cancellationToken)
    {
        return await _context.Products.Include(product => product.ProductCategory)
            .OrderBy(product => product.Price)
            .Select(product => ProductMapper.MapToDto(product))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    ///     Возвращает сортированные продукты по уменьшению цены.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов по цене</returns>
    private async Task<List<ProductDto>> GetProductByPriceDesc(CancellationToken cancellationToken)
    {
        return await _context.Products.Include(product => product.ProductCategory)
            .OrderByDescending(product => product.Price)
            .Select(product => ProductMapper.MapToDto(product))
            .ToListAsync(cancellationToken);
    }
}