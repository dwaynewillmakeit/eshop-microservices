
using CatlogApi.Products.CreateProduct;

namespace CatlogApi.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public record UpdateProductRequest(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price);

        public record UpdateProductResponse(bool IsSuccess);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateProductRequest request, ISender sender) => {
            
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return response;
            })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product")
                .WithDescription("Update a product");
        }
    }
}
