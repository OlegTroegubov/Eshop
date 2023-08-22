using Eshop.Application.Features.ProductCategories.Dtos;
using Eshop.Domain.Entities;

namespace Eshop.Application.Features.Products.Dtos;

public static class ProductMapper
{
    public static List<Product> ListDtoMapToProduct(List<ProductDto> productDtos)
    {
        return productDtos.Select(MapToProduct).ToList();
    }

    public static List<ProductDto> ListProductMapToDto(List<Product> products)
    {
        return products.Select(MapToDto).ToList();
    }

    public static ProductDto MapToDto(Product product)
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

    public static Product MapToProduct(ProductDto productDto)
    {
        if (productDto == null)
            return null;

        return new Product
        {
            Id = productDto.Id,
            ProductCategoryId = productDto.ProductCategoryId,
            Title = productDto.Title,
            Price = Convert.ToDecimal(productDto.Price.TrimEnd('₽')),
            ProductCategory = ProductCategoryMapper.MapToProductCategory(productDto.ProductCategory)
        };
    }
}