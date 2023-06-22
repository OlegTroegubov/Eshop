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
    /// Внешний ключ для связи с Клиентом
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Связанный список Продуктов
    /// </summary>
    public List<Product> Products { get; private set; }
    
    /// <summary>
    /// Связанный объект Клиент
    /// </summary>
    public Client Client { get; private set; }
}