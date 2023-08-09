using System.Globalization;
using Eshop.Dtos.Product;
using Eshop.Services;
using MediatR;

namespace Eshop.Features.Product.Commands;

public record CreateProductCommand(int ProductCategoryId, string Title, decimal Price) : IRequest<ProductDto>;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly ProductService _productService;

    public CreateProductHandler(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.AddAsync(new ProductDto
        {
            ProductCategoryId = request.ProductCategoryId,
            Title = request.Title,
            Price = request.Price.ToString(CultureInfo.InvariantCulture),
        }, cancellationToken);
    }
}