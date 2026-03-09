namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category): IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryQueryHandler
        (IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger)
        : IRequestHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);
            
            if (products is null || !products.Any())
            {
                logger.LogInformation("No products found for category {Category}", query.Category);

                throw new ProductsNotFoundByCategoriesException(query.Category);
            }   
            return new GetProductsByCategoryResult(products);   
        }


    }
}
