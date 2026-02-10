namespace StoreApp.Data.Concrete;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new List<Product>();

    public static string GetCategoryName(Product product)
    {
        return product.Categories != null && product.Categories.Any()
            ? string.Join(", ", product.Categories.Select(c => c.Name))
            : string.Empty;
    }
}
