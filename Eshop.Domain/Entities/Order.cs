﻿namespace Eshop.Domain.Entities;

/// <summary>
///     Заказ
/// </summary>
public class Order
{
    public Order()
    {
        DateOfOrderTime = DateTime.Now;
    }

    /// <summary>
    ///     Первичный ключ для Заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Внешний ключ для связи с Клиентом
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    ///     Дата заказа
    /// </summary>
    public DateTime DateOfOrderTime { get; set; }

    /// <summary>
    ///     Сумма заказа
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Связанный объект Клиент
    /// </summary>
    public Client Client { get; }
}