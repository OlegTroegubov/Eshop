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
    ///     Возвращает сортированные продукты.
    /// </summary>
    /// <param name="propertyName">Имя параметра для сортировки(свойство продукта).</param>
    /// <param name="sortOrder">Значение сортировки(asc или desc).</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Список сортированных продуктов</returns>
    public async Task<IActionResult> GetSortedProducts(string propertyName, string sortOrder,
        CancellationToken cancellationToken)
    {
        return Json(await _productService.GetSortedProductsAsync(propertyName, sortOrder, cancellationToken));
    }

    /// <summary>
    ///     Возвращает список продуктов.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        return Json(await _productService.GetProductsAsync(cancellationToken));
    }

    /// <summary>
    ///     Создает новый продукт.
    /// </summary>
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
    ///     Отображает страницу с подробной информацией по указанному идентификатору.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        return View(await _productService.GetProductByIdAsync(id, cancellationToken));
    }

    /// <summary>
    ///     Удаляет продукт.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Изменяет продукт.
    /// </summary>
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