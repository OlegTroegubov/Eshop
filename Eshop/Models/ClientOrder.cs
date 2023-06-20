namespace Eshop.Models;

public class ClientOrder
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int OrderId { get; set; }

    public Client Client { get; private set; }
    public Order Order { get; private set; }
}