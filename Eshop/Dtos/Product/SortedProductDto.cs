namespace Eshop.Dtos.Product;

public class SortedProductDto
{
    public List<ProductDto> Products { get; set; }
    public string PropertyName { get; set; }
    public string SortOrder { get; set; }
}