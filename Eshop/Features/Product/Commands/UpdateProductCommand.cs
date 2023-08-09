using System.Globalization;
using Eshop.Dtos.Product;
using Eshop.Services;
using MediatR;

namespace Eshop.Features.Product.Commands;

public record UpdateProductCommand(int Id, int ProductCategoryId, string Title, decimal Price) : IRequest;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly ProductService _productService;

    public UpdateProductHandler(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _productService.EditAsync(new ProductDto
        {
            Id = request.Id,
            ProductCategoryId = request.ProductCategoryId,
            Title = request.Title,
            Price = request.Price.ToString(CultureInfo.InvariantCulture)
        }, cancellationToken);

        return Unit.Value;
    }
}