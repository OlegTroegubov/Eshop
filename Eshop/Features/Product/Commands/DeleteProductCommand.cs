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
        _context.Products.Remove(
            await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}