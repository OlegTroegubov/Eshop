using System.ComponentModel.DataAnnotations;
using Eshop.Exceptions;
using Eshop.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Features.Product.Commands;

/// <summary>
///     Команда для удаления продукта.
/// </summary>
/// <param name="Id">Первичный ключ продукта.</param>
public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteProductCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException("Продукт не найден!");
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}