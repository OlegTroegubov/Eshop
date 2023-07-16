﻿namespace Eshop.Dtos.ProductCategory;

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
    ///     Связанный объект категории
    /// </summary>
    public ProductCategoryDto ParentProductCategory { get; set; }
}