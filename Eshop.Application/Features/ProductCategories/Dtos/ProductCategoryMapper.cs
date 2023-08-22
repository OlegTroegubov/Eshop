using Eshop.Domain.Entities;

namespace Eshop.Application.Features.ProductCategories.Dtos;

public static class ProductCategoryMapper
{
    public static ProductCategoryDto MapToDto(ProductCategory? productCategory)
    {
        if (productCategory == null)
            return null;

        return new ProductCategoryDto
        {
            Id = productCategory.Id,
            Name = productCategory.Name,
            IsLastInHierarchy = productCategory.IsLastInHierarchy,
            ParentProductCategory = MapToDto(productCategory.ParentProductCategory)
        };
    }

    public static ProductCategory MapToProductCategory(ProductCategoryDto? productCategoryDto)
    {
        if (productCategoryDto == null)
            return null;

        return new ProductCategory
        {
            Id = productCategoryDto.Id,
            Name = productCategoryDto.Name,
            IsLastInHierarchy = productCategoryDto.IsLastInHierarchy,
            ParentProductCategoryId = productCategoryDto.ParentProductCategory?.Id,
            ParentProductCategory = MapToProductCategory(productCategoryDto.ParentProductCategory)
        };
    }
}