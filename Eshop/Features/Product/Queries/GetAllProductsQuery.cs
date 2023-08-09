using Eshop.Dtos.Product;
using Eshop.Services;
using MediatR;

namespace Eshop.Features.Product.Queries;

public record GetAllProductsQuery(int CategoryId, string SortName, string SortOrder) : IRequest<List<ProductDto>>;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly ProductService _productService;

    public GetAllProductsHandler(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetProductsAsync(request.CategoryId, request.SortName, request.SortOrder,
            cancellationToken);
    }
}