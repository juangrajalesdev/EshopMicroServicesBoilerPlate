namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id): IQuery<GetProductByIdResut>;
    public record GetProductByIdResut(Product Product);

    internal class GetProductByIdQueryHandler
        (IDocumentSession session,ILogger <GetProductByIdQueryHandler> logger)
        : IRequestHandler<GetProductByIdQuery, GetProductByIdResut>
    {
        public async Task<GetProductByIdResut> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.id, cancellationToken);
            if (product is null)
            {
                logger.LogWarning("Product with id {id} not found", query.id);
                throw new ProductNotFoundException(query.id);
            }
            return new GetProductByIdResut(product);
        }


    }
}
