namespace Eshop.Models;
/// <summary>
/// Таблица для связи Клиента и Заказа
/// </summary>
public class ClientOrder
{
    /// <summary>
    /// Первичный ключ для записи Клиент-Заказ
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Внешний ключ для связи с Клиентом
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Внешний ключ для связи с Заказом
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Связанный объект Клиент
    /// </summary>
    public Client Client { get; private set; }
    
    /// <summary>
    /// Связанный объект Заказ
    /// </summary>
    public Order Order { get; private set; }
}