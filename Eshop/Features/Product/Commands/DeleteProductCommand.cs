using Eshop.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Features.Product.Commands;

/// <summary>
///     Команда для удаления продукта.
/// </summary>
/// <param name="Id">Первичный ключ продукта.</param>
public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteProductHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _context.Products.Remove(
            await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}