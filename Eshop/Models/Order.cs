namespace Eshop.Models;

/// <summary>
/// Заказ
/// </summary>
public class Order
{
    /// <summary>
    /// Первичный ключ для Заказа
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Внешний ключ для связи с Продуктом
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Связанный объект Продукт
    /// </summary>
    public Product Product { get; private set; }
}