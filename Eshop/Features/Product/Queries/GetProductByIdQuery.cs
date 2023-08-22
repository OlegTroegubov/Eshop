using System.ComponentModel.DataAnnotations;
using Eshop.Dtos.Mappers;
using Eshop.Dtos.Product;
using Eshop.Exceptions;
using Eshop.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Features.Product.Queries;

/// <summary>
///     Команда для поиска продукта по первичному ключу.
/// </summary>
/// <param name="Id">Первичный ключ продукта.</param>
public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly ApplicationDbContext _context;

    public GetProductByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException("Продукт не найден!");
        }

        return ProductMapper.MapToDto(product);
    }
}