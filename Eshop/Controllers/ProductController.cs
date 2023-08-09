using Eshop.Dtos.Product;
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
    /// <returns>Список всех продуктов.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts(int categoryId, string sortName, string sortOrder)
    {
        return Json(await _mediator.Send(new GetAllProductsQuery(categoryId, sortName, sortOrder)));
    }

    /// <summary>
    ///     Добавляет продукт.
    /// </summary>
    /// <param name="command">Команда для создания продукта</param>
    /// <returns>Модель продукта Dto добавленного продукта</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var productDto = await _mediator.Send(command);
        return Json(new { redirectUrl = Url.Action("Details", new { id = productDto.Id }) });
    }

    /// <summary>
    ///     Получает продукт по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор продукта.</param>
    /// <returns>Модель продукта Dto с указанным идентификатором.</returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        return View(await _mediator.Send(new GetProductByIdQuery(id)));
    }

    /// <summary>
    ///     Удаляет продукт по указанному идентификатору.
    /// </summary>
    /// <param name="command">Команда для удаления продукта</param>
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteProductCommand command)
    {
        await _mediator.Send(command);
        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Изменяет продукт.
    /// </summary>
    /// <param name="command">Команда для обновления продукта</param>
    /// <returns>Модель продукта Dto изменненого продукта</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateProductCommand command)
    {
        await _mediator.Send(command);
        return Json(new { redirectUrl = Url.Action("Details", new { id = command.Id }) });
    }
}