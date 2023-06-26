using System.ComponentModel.DataAnnotations;

namespace Eshop.Models;

/// <summary>
/// Продукт
/// </summary>
public class Product
{
    /// <summary>
    /// Первичный ключ для Продукта
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Названание Продукта
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите название продукта.")]
    [StringLength(100,MinimumLength = 3,ErrorMessage = "Длина названия продукта должна содержать от 2 до 100 символов.")]
    public string Title { get; set; }
    
    /// <summary>
    /// Стоимость Продукта
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите стоимость продукта.")]
    [Range(1, double.MaxValue, ErrorMessage = "Значение стоимости должно быть больше 0.")]
    public decimal Price { get; set; }
}