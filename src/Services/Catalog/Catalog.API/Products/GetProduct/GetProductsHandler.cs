using Marten.Pagination;
using System.Collections;

namespace Catalog.API.Products.GetProduct
{

    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler
        (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all products");

            var products = await session.Query<Product>()
            .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
