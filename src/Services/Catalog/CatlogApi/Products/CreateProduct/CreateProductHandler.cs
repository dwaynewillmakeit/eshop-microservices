using BuildingBlocks.CQRS;
using CatlogApi.Models;
using MediatR;
using System.Xml.Linq;

namespace CatlogApi.Products.CreateProduct;

public record CreateProductComand(string Name, string Description, List<string> Category, string ImageFile) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductComand, CreateProductResult>
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

        //Return result

        return new CreateProductResult(Guid.NewGuid());

    }
}

