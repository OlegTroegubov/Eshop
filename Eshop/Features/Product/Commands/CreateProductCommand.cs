using Eshop.Exceptions;
using Eshop.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var alreadyExists =
            await _context.Products.AnyAsync(product => product.Title == request.Title, cancellationToken);

        if (alreadyExists)
        {
            throw new NotFoundException("Продукт с таким наименованием уже существует!");
        }

        var product = await _context.Products.AddAsync(new Models.Product
        {
            ProductCategoryId = request.ProductCategoryId,
            Title = request.Title,
            Price = request.Price
        }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product.Entity.Id;
    }
}