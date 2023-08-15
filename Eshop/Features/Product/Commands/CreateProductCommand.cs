using System.Globalization;
using Eshop.Dtos.Mappers;
using Eshop.Dtos.Product;
using Eshop.Persistence;
using MediatR;

namespace Eshop.Features.Product.Commands;

/// <summary>
///     Команда для создания продукта.
/// </summary>
/// <param name="ProductCategoryId">Первичный ключ категории продукта.</param>
/// <param name="Title">Наименование продукта.</param>
/// <param name="Price">Стоимость продукта.</param>
public record CreateProductCommand(int ProductCategoryId, string Title, decimal Price) : IRequest<int>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateProductCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = ProductMapper.MapToProduct(new ProductDto
        {
            ProductCategoryId = request.ProductCategoryId,
            Title = request.Title,
            Price = request.Price.ToString(CultureInfo.InvariantCulture)
        });
        await _context.Products.AddAsync(productToAdd, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return productToAdd.Id;
    }
}