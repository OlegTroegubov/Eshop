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
    /// <param name="products"></param>
    /// <param name="propertyName">Имя параметра для сортировки(свойство продукта).</param>
    /// <param name="sortOrder">Значение сортировки(asc или desc).</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов</returns>
    public async Task<List<ProductDto>> GetSortedProductsAsync(List<ProductDto> products, string propertyName,
        string sortOrder,
        CancellationToken cancellationToken)
    {
        switch (propertyName.ToLower())
        {
            case "price":
                if (sortOrder == "desc") return GetProductByPriceDesc(products);
                return GetProductByPriceAsc(products);

            case "title":
                if (sortOrder == "desc") return GetProductByTitleDesc(products);
                return GetProductByTitleAsc(products);

            default: return await GetProductsAsync(cancellationToken);
        }
    }

    /// <summary>
    ///     Возвращает отфильтрованные продукты по категории.
    /// </summary>
    /// <param name="categoryId">Id категории, по которой идет поиск</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список отфильтрованных продуктов по категории</returns>
    public async Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Include(product => product.ProductCategory)
            .ToListAsync(cancellationToken);

        if (categoryId == 0) return ProductMapper.ListProductMapToDto(products);

        return products.Where(product => IsContainsCategory(product.ProductCategory, categoryId).Result)
            .Select(ProductMapper.MapToDto).ToList();
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
        await IncludeCategories(productCategory);
        if (productCategory == null) return false;

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

    /// <summary>
    ///     Возвращает сортированные продукты по увеличению по имени.
    /// </summary>
    /// <param name="products">Список продуктов, который требуется отсортировать</param>
    /// <returns>Список сортированных продуктов по имени</returns>
    private List<ProductDto> GetProductByTitleAsc(List<ProductDto> products)
    {
        return ProductMapper.ListDtoMapToProduct(products)
            .OrderBy(product => product.Title)
            .Select(ProductMapper.MapToDto)
            .ToList();
    }

    /// <summary>
    ///     Возвращает сортированные продукты по уменьшению по имени.
    /// </summary>
    /// <param name="products">Список продуктов, который требуется отсортировать</param>
    /// <returns>Список сортированных продуктов по имени</returns>
    private List<ProductDto> GetProductByTitleDesc(List<ProductDto> products)
    {
        return ProductMapper.ListDtoMapToProduct(products)
            .OrderByDescending(product => product.Title)
            .Select(ProductMapper.MapToDto)
            .ToList();
    }

    /// <summary>
    ///     Возвращает сортированные продукты по увеличению цены.
    /// </summary>
    /// <param name="products">Список продуктов, который требуется отсортировать</param>
    /// <returns>Список сортированных продуктов по цене</returns>
    private List<ProductDto> GetProductByPriceAsc(List<ProductDto> products)
    {
        return ProductMapper.ListDtoMapToProduct(products)
            .OrderBy(product => product.Price)
            .Select(ProductMapper.MapToDto)
            .ToList();
    }

    /// <summary>
    ///     Возвращает сортированные продукты по уменьшению цены.
    /// </summary>
    /// <param name="products">Список продуктов, который требуется отсортировать</param>
    /// <returns>Список сортированных продуктов по цене</returns>
    private List<ProductDto> GetProductByPriceDesc(List<ProductDto> products)
    {
        return ProductMapper.ListDtoMapToProduct(products)
            .OrderByDescending(product => product.Price)
            .Select(ProductMapper.MapToDto)
            .ToList();
    }

    #endregion
}