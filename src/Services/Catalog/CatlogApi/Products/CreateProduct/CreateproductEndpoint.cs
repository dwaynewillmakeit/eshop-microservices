namespace CatlogApi.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFile);

    public record CreateProductResponse(Guid id);

    public class CreateproductEndpoint
    {
    }
}
