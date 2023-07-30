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
    /// <param name="categoryId"> Id категории, по которой идет фильтраци</param>
    /// <param name="sortName">Имя свойства продукта для сортировки</param>
    /// <param name="sortOrder">Направление сортировки(asc или desc)</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список всех продуктов.</returns>
    public async Task<List<ProductDto>> GetProductsAsync(int categoryId, string sortName,
        string sortOrder, CancellationToken cancellationToken)
    {
        var products = _context.Products.AsQueryable();
        if (categoryId != 0)
        {
            var category = await _context.ProductCategories.AsNoTracking()
                .Include(productCategory => productCategory.ChildrenProductCategory)
                .ThenInclude(productCategory => productCategory.ChildrenProductCategory)
                .FirstOrDefaultAsync(productCategory => productCategory.Id == categoryId, cancellationToken);

            var categoryIds = category.ChildrenProductCategory
                .SelectMany(childCategory => childCategory.ChildrenProductCategory
                    .Select(grandchildCategory => grandchildCategory.Id)
                    .Union(new List<int> { childCategory.Id }))
                .ToList();
            categoryIds.Add(categoryId);

            products = products.Where(product => categoryIds.Contains(product.ProductCategoryId));
        }

        if (!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder))
            products = sortName switch
            {
                "price" => sortOrder switch
                {
                    "asc" => products.OrderBy(product => product.Price),
                    "desc" => products.OrderByDescending(product => product.Price),
                    _ => products
                },
                "title" => sortOrder switch
                {
                    "asc" => products.OrderBy(product => product.Title),
                    "desc" => products.OrderByDescending(product => product.Title),
                    _ => products
                },
                _ => products
            };

        return await products
            .Include(product => product.ProductCategory)
            .Select(product => ProductMapper.MapToDto(product)).ToListAsync(cancellationToken);
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
}