using Eshop.Dtos.Mappers;
using Eshop.Dtos.Product;
using Eshop.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Features.Product.Queries;

/// <summary>
///     Команда для получения списка продуктов.
/// </summary>
/// <param name="CategoryId">Ключ категории продукта.</param>
/// <param name="SortName">Наименования поля продукта, по которому идет сортировка.</param>
/// <param name="SortOrder">Направление сортировки(desc - по убыванию, asc - по возрастанию).</param>
public record GetAllProductsQuery(int CategoryId, string SortName, string SortOrder) : IRequest<List<ProductDto>>;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllProductsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products.AsQueryable();
        if (request.CategoryId != 0)
        {
            var category = await _context.ProductCategories.AsNoTracking()
                .Include(productCategory => productCategory.ChildrenProductCategory)
                .ThenInclude(productCategory => productCategory.ChildrenProductCategory)
                .FirstOrDefaultAsync(productCategory => productCategory.Id == request.CategoryId, cancellationToken);

            var categoryIds = category.ChildrenProductCategory
                .SelectMany(childCategory => childCategory.ChildrenProductCategory
                    .Select(grandchildCategory => grandchildCategory.Id)
                    .Union(new List<int> { childCategory.Id }))
                .ToList();
            categoryIds.Add(request.CategoryId);

            products = products.Where(product => categoryIds.Contains(product.ProductCategoryId));
        }

        if (!string.IsNullOrEmpty(request.SortName) && !string.IsNullOrEmpty(request.SortOrder))
            products = request.SortName switch
            {
                "price" => request.SortOrder switch
                {
                    "asc" => products.OrderBy(product => product.Price),
                    "desc" => products.OrderByDescending(product => product.Price),
                    _ => products
                },
                "title" => request.SortOrder switch
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
}