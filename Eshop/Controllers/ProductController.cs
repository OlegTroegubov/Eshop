using Eshop.Dtos.Product;
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

    /// <summary>
    ///     Отображает главную страницу, со списком всех продуктов.
    /// </summary>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    ///     Получает список всех продуктов.
    /// </summary>
    /// <param name="categoryId"> Id категории, по которой идет фильтраци</param>
    /// <param name="sortName">Имя свойства продукта для сортировки</param>
    /// <param name="sortOrder">Направление сортировки(asc или desc)</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список всех продуктов.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts(int categoryId, string sortName, string sortOrder,
        CancellationToken cancellationToken)
    {
        return Json(await _productService.GetProductsAsync(categoryId, sortName, sortOrder, cancellationToken));
    }

    /// <summary>
    ///     Добавляет продукт.
    /// </summary>
    /// <param name="product">Продукт.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель продукта Dto добавленного продукта</returns>
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

    /// <summary>
    ///     Получает продукт по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор продукта.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель продукта Dto с указанным идентификатором.</returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        return View(await _productService.GetProductByIdAsync(id, cancellationToken));
    }

    /// <summary>
    ///     Удаляет продукт по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемоего продукта.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Изменяет продукт.
    /// </summary>
    /// <param name="product">Продукт.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Модель продукта Dto добавленного продукта</returns>
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