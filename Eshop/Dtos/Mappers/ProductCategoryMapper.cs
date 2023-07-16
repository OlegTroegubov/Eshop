using Eshop.Dtos.ProductCategory;

namespace Eshop.Dtos.Mappers;

public static class ProductCategoryMapper
{
    public static ProductCategoryDto MapToDto(Models.ProductCategory productCategory)
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

    public static Models.ProductCategory MapToProductCategory(ProductCategoryDto productCategoryDto)
    {
        if (productCategoryDto == null)
            return null;

        return new Models.ProductCategory
        {
            Id = productCategoryDto.Id,
            Name = productCategoryDto.Name,
            IsLastInHierarchy = productCategoryDto.IsLastInHierarchy,
            ParentProductCategoryId = productCategoryDto.ParentProductCategory?.Id,
            ParentProductCategory = MapToProductCategory(productCategoryDto.ParentProductCategory)
        };
    }
}