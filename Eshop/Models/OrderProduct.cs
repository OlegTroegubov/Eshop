namespace Eshop.Models;

/// <summary>
/// Класс для связи заказа с продуктами
/// </summary>
public class OrderProduct
{
    /// <summary>
    /// Первичный ключ 
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Внешний ключ для связи с Заказом
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Внешний ключ для связи с Продуктом
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Связанный объект Заказ
    /// </summary>
    public Order Order { get;}
    
    /// <summary>
    /// Связанный объект Продукт
    /// </summary>
    public Product Product { get;}
}