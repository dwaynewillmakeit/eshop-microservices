namespace CatlogApi.Products.CreateProduct;
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFile);

    public record CreateProductResponse(Guid id);

public class CreateproductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", 
            async (CreateProductRequest request, ISender sender) => 
            {
                var command = request.Adapt<CreateProductComand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.id}",response);
            }
            )
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create a product")
            ;

        throw new NotImplementedException();
    }
}

