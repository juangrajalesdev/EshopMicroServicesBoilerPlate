using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.DeleteProductById
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool isSuccess);
    public class DeleteProductByIdQueryHandler
        (IDocumentSession session,ILogger<DeleteProductByIdQueryHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductByIdResult>

    {
        public async Task <DeleteProductByIdResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductByIdQueryHandler called with command: {@command}", command);

            session.Delete<Product>(command.id);
            await session.SaveChangesAsync();

            return new DeleteProductByIdResult(true);
        }

    }
}
