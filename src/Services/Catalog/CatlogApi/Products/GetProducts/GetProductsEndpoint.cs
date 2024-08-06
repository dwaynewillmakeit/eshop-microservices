
namespace CatlogApi.Products.GetProducts
{
    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) => {

                var result = await sender.Send(new GetProductsQuery());

                var response =  result.Adapt<GetProductResponse>(); 

                return Results.Ok(response);
            })
                .WithName("GetProduct")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .WithDescription("Get Products")
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                ;
        }
    }
}
