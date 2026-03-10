namespace Catalog.API.Products.DeleteProductById
{
    public record DeleteProductByIdRequest(Guid Id);
    public record DeleteProductResponse(Guid Id);
    public class DeleteProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                
                var response = result.Adapt<DeleteProductResponse>();

                return Results.NoContent();
            })
            .WithName("DeleteProductById")
            .WithTags("Products")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product By Id")
            .WithDescription("Deletes a product by its unique identifier.");
        }

    }
}
