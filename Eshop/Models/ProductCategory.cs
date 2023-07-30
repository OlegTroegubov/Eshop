namespace Eshop.Models;

/// <summary>
///     Класс для категорий товара
/// </summary>
public class ProductCategory
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
    public ProductCategory ParentProductCategory { get; set; }

    /// <summary>
    ///     Связанный список дочерних категорий
    /// </summary>
    public List<ProductCategory> ChildrenProductCategory { get; set; }
}