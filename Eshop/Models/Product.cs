namespace Eshop.Models;

/// <summary>
///     Продукт
/// </summary>
public class Product
{
    /// <summary>
    ///     Первичный ключ для Продукта
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Внешний ключ для категорий товара
    /// </summary>
    public int ProductCategoryId { get; set; }

    /// <summary>
    ///     Названание Продукта
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Стоимость Продукта
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Промежуточный объект категории
    /// </summary>
    public ProductCategory ProductCategory { get; set; }
}