using Eshop.Services;
using MediatR;

namespace Eshop.Features.Product.Commands;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly ProductService _productService;

    public DeleteProductHandler(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}