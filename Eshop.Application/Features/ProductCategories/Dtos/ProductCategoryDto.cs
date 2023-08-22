namespace Eshop.Application.Features.ProductCategories.Dtos;

/// <summary>
///     Промежуточная модель категории товара
/// </summary>
public class ProductCategoryDto
{
    /// <summary>
    ///     Первичный ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Внешний ключ для родительской категории
    /// </summary>
    public int? ParentProductCategoryId { get; set; }

    /// <summary>
    ///     Название категории
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Является ли категория последней в иерархии
    /// </summary>
    public bool IsLastInHierarchy { get; set; }

    /// <summary>
    ///     Связанный объект категории
    /// </summary>
    public ProductCategoryDto ParentProductCategory { get; set; }
}