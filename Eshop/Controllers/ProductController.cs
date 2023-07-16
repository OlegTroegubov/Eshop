﻿using Eshop.Dtos.Product;
using Eshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers;

public class ProductController : Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    // GET: Product
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await _productService.GetProductsAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .ToDictionary(x => x.Key, x => x.Value.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray());
            return BadRequest(errors);
        }

        var createdProduct = await _productService.AddAsync(product, cancellationToken);
        return Json(new { redirectUrl = Url.Action("Details", new { id = createdProduct.Id }) });
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        return View(await _productService.GetProductByIdAsync(id, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDto product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .ToDictionary(x => x.Key, x => x.Value.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray());
            return BadRequest(errors);
        }

        await _productService.EditAsync(product, cancellationToken);
        return Json(new { redirectUrl = Url.Action("Details", new { id = product.Id }) });
    }
}