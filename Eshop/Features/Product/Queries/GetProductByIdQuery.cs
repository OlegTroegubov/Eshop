using Eshop.Dtos.Mappers;
using Eshop.Dtos.Product;
using Eshop.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Features.Product.Queries;

/// <summary>
///     Команда для поиска продукта по первичному ключу.
/// </summary>
/// <param name="Id">Первичный ключ продукта.</param>
public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly ApplicationDbContext _context;

    public GetProductByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return ProductMapper.MapToDto(await _context.Products
            .Include(product => product.ProductCategory)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken));
    }
}