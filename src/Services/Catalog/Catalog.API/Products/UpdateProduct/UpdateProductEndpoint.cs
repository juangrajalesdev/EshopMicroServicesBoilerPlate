namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id,string Name, List<string> Categories,string Description,string ImageFile,decimal Price);
    public record UpdateProductResponse(bool isSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request , ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);

                return Results.Ok(result.Adapt<UpdateProductResponse>());
            })
            .WithName("UpdateProduct")
            .WithTags("Products")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Updates a product by its unique identifier.");
        }
    }
}
