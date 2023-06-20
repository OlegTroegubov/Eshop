namespace Eshop.Models;

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    
    public Product Product { get; private set; }
}