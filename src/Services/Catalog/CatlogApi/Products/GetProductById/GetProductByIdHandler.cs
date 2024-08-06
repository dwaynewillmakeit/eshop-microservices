using Marten.Linq.QueryHandlers;

namespace CatlogApi.Products.GetProductById
{
    public record GetProductByIdResult(Product Product);
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler>_logger
        )
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Getting product by ID {@Query}", query);

            var product = await session.LoadAsync<Product>(query.id,cancellationToken);

            if(product == null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(product);
        }
    }
}
