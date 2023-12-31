﻿using Eshop.Application.Common.Exceptions;
using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Features.Products.Commands;

/// <summary>
///     Команда для создания продукта.
/// </summary>
/// <param name="ProductCategoryId">Первичный ключ категории продукта.</param>
/// <param name="Title">Наименование продукта.</param>
/// <param name="Price">Стоимость продукта.</param>
public record CreateProductCommand(int ProductCategoryId, string Title, decimal Price) : IRequest<int>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var alreadyExists =
            await _context.Products.AnyAsync(product => product.Title == request.Title, cancellationToken);

        if (alreadyExists) throw new ExistsException("Продукт с таким наименованием уже существует!");

        var product = await _context.Products.AddAsync(new Product
        {
            ProductCategoryId = request.ProductCategoryId,
            Title = request.Title,
            Price = request.Price
        }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product.Entity.Id;
    }
}