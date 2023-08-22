using System.ComponentModel.DataAnnotations;
using Eshop.Exceptions;
using Eshop.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Features.Product.Commands;

/// <summary>
///     Команда для изменения продукта.
/// </summary>
/// <param name="Id">Первичный ключ продукта.</param>
/// <param name="ProductCategoryId">Первичный ключ категории продукта.</param>
/// <param name="Title">Наименование продукта.</param>
/// <param name="Price">Стоимость продукта.</param>
public record UpdateProductCommand(int Id, int ProductCategoryId, string Title, decimal Price) : IRequest;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateProductCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException("Продукт не найден!");
        }

        product.ProductCategoryId = request.ProductCategoryId;
        product.Price = request.Price;
        product.Title = request.Title;
        
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}