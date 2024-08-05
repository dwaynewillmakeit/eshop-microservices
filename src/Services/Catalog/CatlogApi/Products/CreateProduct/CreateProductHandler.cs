using MediatR;

namespace CatlogApi.Products.CreateProduct;

public record CreateProductComand(string Name, string Description, List<string> Category, string ImageFile):IRequest<CreateProductResult>;
public record CreateProductResult(Guid id);

internal class CreateProductCommandHandler : IRequestHandler<CreateProductComand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductComand request, CancellationToken cancellationToken)
    {
        //Business logic to create a product
        throw new NotImplementedException();
    }
}

