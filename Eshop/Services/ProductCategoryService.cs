using Eshop.Dtos.ProductCategory;
using Eshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Services;

public class ProductCategoryService
{
    private readonly ApplicationDbContext _context;

    public ProductCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Получает иерархию категорий продукта.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Иерархию категорий.</returns>
    public async Task<List<ProductCategoryHierarchyDto>> GetHierarchy(CancellationToken cancellationToken)
    {
        var productCategories = await _context.ProductCategories
            .Include(category => category.ParentProductCategory)
            .ToListAsync(cancellationToken);

        var hierarchyList = new List<ProductCategoryHierarchyDto>();

        foreach (var category in productCategories)
            if (category.ParentProductCategoryId == null)
            {
                var hierarchy = BuildHierarchy(category, productCategories);
                hierarchyList.Add(hierarchy);
            }

        return hierarchyList;
    }

    /// <summary>
    ///     Строит иерархию для каждой категории.
    /// </summary>
    private static ProductCategoryHierarchyDto BuildHierarchy(ProductCategory productCategory,
        List<ProductCategory> productCategories)
    {
        var hierarchy = new ProductCategoryHierarchyDto
        {
            Id = productCategory.Id,
            Name = productCategory.Name,
            ChildrenCategories = new List<ProductCategoryHierarchyDto>()
        };

        foreach (var category in productCategories)
            if (category.ParentProductCategoryId == productCategory.Id)
            {
                var childDto = BuildHierarchy(category, productCategories);
                hierarchy.ChildrenCategories.Add(childDto);
            }

        return hierarchy;
    }
}