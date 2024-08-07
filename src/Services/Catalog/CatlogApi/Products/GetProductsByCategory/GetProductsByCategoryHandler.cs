namespace CatlogApi.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(String Category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductsByCategoryQueryHandler(
        IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> _logger
        ) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting products by category",query);

            var products =  await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync();



            return new GetProductsByCategoryResult(products);
        }
    }
}
