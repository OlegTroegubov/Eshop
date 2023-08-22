namespace Eshop.Application.Features.ProductCategories.Dtos;

public class ProductCategoryHierarchyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProductCategoryHierarchyDto> ChildrenCategories { get; set; }
}