using Eshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers;

public class ProductCategoryController : Controller
{
    private readonly ProductCategoryService _categoryService;

    public ProductCategoryController(ProductCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHierarchy(CancellationToken cancellationToken)
    {
        return Json(await _categoryService.GetHierarchy(cancellationToken));
    }
}