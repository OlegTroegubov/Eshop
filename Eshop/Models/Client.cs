namespace Eshop.Models;

public class Client
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string SecondName { get; set; }
    public string FirstName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
}