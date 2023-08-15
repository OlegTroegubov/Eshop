using Eshop.Features.Product.Commands;
using Eshop.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers;

public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
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
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Список всех продуктов.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts(int categoryId, string sortName, string sortOrder,
        CancellationToken cancellationToken)
    {
        return Json(await _mediator.Send(new GetAllProductsQuery(categoryId, sortName, sortOrder), cancellationToken));
    }

    /// <summary>
    ///     Добавляет продукт.
    /// </summary>
    /// <param name="command">Команда для создания продукта</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Модель продукта Dto добавленного продукта</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
    {
        return Json(new
            { redirectUrl = Url.Action("Details", new { id = await _mediator.Send(command, cancellationToken) }) });
    }

    /// <summary>
    ///     Получает продукт по указанному идентификатору.
    /// </summary>
    /// <param name="query">Команда для получения продукта по идентификатору</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Модель продукта Dto с указанным идентификатором.</returns>
    [HttpGet]
    public async Task<IActionResult> Details(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        return View(await _mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Удаляет продукт по указанному идентификатору.
    /// </summary>
    /// <param name="command">Команда для удаления продукта</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Изменяет продукт.
    /// </summary>
    /// <param name="command">Команда для обновления продукта</param>
    /// ///
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Модель продукта Dto измененного продукта</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Json(new { redirectUrl = Url.Action("Details", new { id = command.Id }) });
    }
}