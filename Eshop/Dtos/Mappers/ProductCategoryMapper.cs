namespace Eshop.Dtos.Mappers;

using Eshop.Models;
using Eshop.Dtos.ProductCategory;

public static class ProductCategoryMapper
{
    public static ProductCategoryDto MapToDto(ProductCategory productCategory)
    {
        if (productCategory == null)
            return null;

        return new ProductCategoryDto
        {
            Id = productCategory.Id,
            Name = productCategory.Name,
            ParentProductCategory = MapToDto(productCategory.ParentProductCategory)
        };
    }

    public static ProductCategory MapToProductCategory(ProductCategoryDto productCategoryDto)
    {
        if (productCategoryDto == null)
            return null;

        return new ProductCategory
        {
            Id = productCategoryDto.Id,
            Name = productCategoryDto.Name,
            ParentProductCategoryId = productCategoryDto.ParentProductCategory?.Id,
            ParentProductCategory = MapToProductCategory(productCategoryDto.ParentProductCategory)
        };
    }
}