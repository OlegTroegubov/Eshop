using Eshop.Application.Common.Interfaces;
using Eshop.Application.Features.ProductCategories.Dtos;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Features.ProductCategories.Queries;

public record GetProductCategoryHierarchyQuery : IRequest<List<ProductCategoryHierarchyDto>>;

public class
    GetProductCategoryHierarchyQueryHandler : IRequestHandler<GetProductCategoryHierarchyQuery,
        List<ProductCategoryHierarchyDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProductCategoryHierarchyQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductCategoryHierarchyDto>> Handle(GetProductCategoryHierarchyQuery request,
        CancellationToken cancellationToken)
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