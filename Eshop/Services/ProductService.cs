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
        var products = await _context.Products.Include(product => product.ProductCategory).ToListAsync(cancellationToken);

        if (categoryId != 0)
        {
            products = products.Where(product => IsContainsCategory(product.ProductCategory, categoryId).Result).ToList();
        }

        if (!string.IsNullOrEmpty(sortName) && !string.IsNullOrEmpty(sortOrder))
        {
            products = sortName switch
            {
                "price" => sortOrder switch
                {
                    "asc" => products.OrderBy(product => product.Price).ToList(),
                    "desc" => products.OrderByDescending(product => product.Price).ToList(),
                    _ => products
                },
                "title" => sortOrder switch
                {
                    "asc" => products.OrderBy(product => product.Title).ToList(),
                    "desc" => products.OrderByDescending(product => product.Title).ToList(),
                    _ => products
                },
                _ => products
            };
        }

        return products
            .Select(product => ProductMapper.MapToDto(product)).ToList();
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

    #region privateMethods

    /// <summary>
    ///     Проверяет, содержит ли категория категорию, которую мы ищем.
    /// </summary>
    /// <param name="productCategory">Категория продукта.</param>
    /// <param name="categoryId">Id категории, которую мы ищем.</param>
    /// <returns>Результат false - продукт не относится к категории.</returns>
    /// <returns>Результат true - продукт относится к категории</returns>
    private async Task<bool> IsContainsCategory(ProductCategory productCategory, int categoryId)
    {
        if (productCategory == null) return false;
        await IncludeCategories(productCategory);
        if (productCategory.Id == categoryId) return true;

        return await IsContainsCategory(productCategory.ParentProductCategory, categoryId);
    }

    /// <summary>
    ///     Загружает ссылки на родительские категории, если такие имеются.
    /// </summary>
    /// <param name="category">Категория продукта.</param>
    /// <returns>Категорию со всеми вложенными родительскими категорями</returns>
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

    #endregion
}