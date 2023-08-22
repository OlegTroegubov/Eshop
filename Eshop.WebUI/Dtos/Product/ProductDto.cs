using System.ComponentModel.DataAnnotations;
using Eshop.Dtos.ProductCategory;

namespace Eshop.Dtos.Product;

/// <summary>
///     Промежуточная модель продутка
/// </summary>
public class ProductDto
{
    /// <summary>
    ///     Первичный ключ для Продукта
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Внешний ключ для категорий товара
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, выберите категорию продукта.")]
    [Display(Name = "Категория продукта")]
    public int ProductCategoryId { get; set; }

    /// <summary>
    ///     Названание Продукта
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите название продукта.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Длина названия продукта должна содержать от 2 до 100 символов.")]
    [Display(Name = "Название товара")]
    public string Title { get; set; }

    /// <summary>
    ///     Стоимость Продукта
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите стоимость продукта.")]
    [Range(1, double.MaxValue, ErrorMessage = "Значение стоимости должно быть больше 0.")]
    [Display(Name = "Стоимость")]
    public string Price { get; set; }

    /// <summary>
    ///     Связанный объект Категория
    /// </summary>

    public ProductCategoryDto ProductCategory { get; set; }
}