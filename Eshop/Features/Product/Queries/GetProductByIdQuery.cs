using Eshop.Dtos.Product;
using Eshop.Services;
using MediatR;

namespace Eshop.Features.Product.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly ProductService _productService;

    public GetProductByIdHandler(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetProductByIdAsync(request.Id, cancellationToken);
    }
}