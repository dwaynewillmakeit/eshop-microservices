
using CatlogApi.Products.GetProducts;

namespace CatlogApi.Products.GetProductById;

public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) => {

            var result = await sender.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();  

            return Results.Ok(response);
        })
            .WithName("GetProductById")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .WithDescription("Get Products By Id")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Id")
                ; ;
    }
}
