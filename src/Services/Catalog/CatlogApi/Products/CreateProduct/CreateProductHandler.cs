
namespace CatlogApi.Products.CreateProduct;

public record CreateProductComand(string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid id);

public class CreateProductComandValidator : AbstractValidator<CreateProductComand>
{
    public CreateProductComandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductComand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductComand command, CancellationToken cancellationToken)
    {
        //Business logic to create a product

        //Create product entity        

        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            ImageFile = command.ImageFile
        };


        //Save to DB
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //Return result

        return new CreateProductResult(product.Id);

    }
}

