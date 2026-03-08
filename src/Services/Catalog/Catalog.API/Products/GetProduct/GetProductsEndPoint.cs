using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductsResponse>();
                
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithTags("Products")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Retrieves a list of products.")   ;
        }

    }
}
