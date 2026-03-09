namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id): IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler
        (IDocumentSession session,ILogger <GetProductByIdQueryHandler> logger)
        : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.id, cancellationToken);
            if (product is null)
            {
                logger.LogWarning("Product with id {id} not found", query.id);
                throw new ProductNotFoundException(query.id);
            }
            return new GetProductByIdResult(product);
        }


    }
}
