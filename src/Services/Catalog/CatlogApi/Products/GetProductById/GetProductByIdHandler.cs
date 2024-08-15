using Marten.Linq.QueryHandlers;

namespace CatlogApi.Products.GetProductById
{
    public record GetProductByIdResult(Product Product);
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public class GetProductByIdQueryHandler(IDocumentSession session
        )
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.id,cancellationToken);

            if(product == null)
            {
                throw new ProductNotFoundException(query.id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
