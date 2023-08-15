using System.Globalization;
using Eshop.Dtos.Mappers;
using Eshop.Dtos.Product;
using Eshop.Models;
using MediatR;

namespace Eshop.Features.Product.Commands;

/// <summary>
///     Команда для изменения продукта.
/// </summary>
/// <param name="Id">Первичный ключ продукта.</param>
/// <param name="ProductCategoryId">Первичный ключ категории продукта.</param>
/// <param name="Title">Наименование продукта.</param>
/// <param name="Price">Стоимость продукта.</param>
public record UpdateProductCommand(int Id, int ProductCategoryId, string Title, decimal Price) : IRequest;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateProductHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _context.Products.Update(ProductMapper.MapToProduct(new ProductDto
        {
            Id = request.Id,
            ProductCategoryId = request.ProductCategoryId,
            Title = request.Title,
            Price = request.Price.ToString(CultureInfo.InvariantCulture)
        }));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}