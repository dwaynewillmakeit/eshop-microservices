
namespace CatlogApi.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
public record DeleteProductResult(bool isSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand> {

    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");


    }

}

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deletoing product by ID using {@Command}", command);

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync();

        return new DeleteProductResult(true);

    }
}
