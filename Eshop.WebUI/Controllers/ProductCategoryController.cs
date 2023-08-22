using Eshop.Application.Features.ProductCategories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers;

public class ProductCategoryController : Controller
{
    private readonly IMediator _mediator;

    public ProductCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetHierarchy(GetProductCategoryHierarchyQuery query,
        CancellationToken cancellationToken)
    {
        return Json(await _mediator.Send(query, cancellationToken));
    }
}