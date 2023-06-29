using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Models
{
    /// <summary>
    /// Класс для категорий товара
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Первичный ключ 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        [Required(ErrorMessage = "Пожалуйста, введите название категории.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия категории должна содержать от 2 до 100 символов.")]
        public string Name { get; set; }
        
    }
}