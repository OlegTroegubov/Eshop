namespace Eshop.Dtos.ProductCategory;

public class ProductCategoryHierarchyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProductCategoryHierarchyDto> ChildrenCategories { get; set; }
}