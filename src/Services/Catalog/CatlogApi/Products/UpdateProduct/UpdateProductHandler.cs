﻿
namespace CatlogApi.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price): ICommand<UpdatedProductResult>;

public record UpdatedProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
        RuleFor(x => x.Name).NotEmpty().Length(2,150).WithMessage("Name must be between 2 and 159 characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is must be greater than zero");
    }
}


internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdatedProductResult>
{
    public async Task<UpdatedProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {        
        var product = await session.LoadAsync<Product>(command.Id,cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
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
