namespace Eshop.Models;

/// <summary>
/// Клиент
/// </summary>
public class Client
{
    /// <summary>
    /// Первичный ключ для Клиента
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Логин Клиента
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль Клиента
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Фамилия Клиента
    /// </summary>
    public string SecondName { get; set; }
    
    /// <summary>
    /// Имя Клиента
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Дата рождения Клиента
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// Номер телефона Клиента
    /// </summary>
    public string Phone { get; set; }
}