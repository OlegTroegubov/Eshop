using Eshop.Dtos.Product;

namespace Eshop.Dtos.Mappers;

public static class ProductMapper
{
    public static ProductDto MapToDto(Models.Product product)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            ProductCategoryId = product.ProductCategoryId,
            Title = product.Title,
            Price = $"{product.Price}₽",
            ProductCategory = ProductCategoryMapper.MapToDto(product.ProductCategory)
        };
    }

    public static Models.Product MapToProduct(ProductDto productDto)
    {
        if (productDto == null)
            return null;

        return new Models.Product
        {
            Id = productDto.Id,
            ProductCategoryId = productDto.ProductCategoryId,
            Title = productDto.Title,
            Price = Convert.ToDecimal(productDto.Price.TrimEnd('₽')),
            ProductCategory = ProductCategoryMapper.MapToProductCategory(productDto.ProductCategory)
        };
    }
}