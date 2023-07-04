using System.ComponentModel.DataAnnotations;

namespace Eshop.Models;

/// <summary>
/// Класс подгатегорий
/// </summary>
public class SubProductCategory
{
    /// <summary>
    /// Первичный ключ 
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название подкатегории
    /// </summary>
    [Required(ErrorMessage = "Пожалуйста, введите название подкатегории.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия подкатегории должна содержать от 2 до 100 символов.")]
    public string Name { get; set; }
}