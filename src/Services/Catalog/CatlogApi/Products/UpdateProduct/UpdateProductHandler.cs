
namespace CatlogApi.Products.UpdateProduct;

public record UpdateProductCommand(Guid id, string Name, string Description, List<string> Category, string ImageFile, decimal Price): ICommand<UpdatedProductResult>;

public record UpdatedProductResult(bool IsSuccess);


internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdatedProductResult>
{
    public async Task<UpdatedProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with command {@Command} ", command);

        var product = await session.LoadAsync<Product>(command.id,cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);

        await session.SaveChangesAsync();

        return new UpdatedProductResult(true);
    }
}
