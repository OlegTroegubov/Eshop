using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Eshop.Models;

/// <summary>
/// Продукт
/// </summary>
[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
public class Product
{
    /// <summary>
    /// Первичный ключ для Продукта
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Внешний ключ для категорий товара
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, выберите категорию продукта.")]
    [Display(Name = "Категория продукта")]
    public int ProductCategoryId { get; set; }
    
    /// <summary>
    /// Названание Продукта
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите название продукта.")]
    [StringLength(100,MinimumLength = 3,ErrorMessage = "Длина названия продукта должна содержать от 2 до 100 символов.")]
    [Display(Name = "Название товара")]
    public string Title { get; set; }
    
    /// <summary>
    /// Стоимость Продукта
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите стоимость продукта.")]
    [Range(1, double.MaxValue, ErrorMessage = "Значение стоимости должно быть больше 0.")]
    [Display(Name = "Стоимость")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Связанный объект Категория
    /// </summary>
    public ProductCategory ProductCategory { get; set; }
}