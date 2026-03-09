namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id,string Name, List<string> Categories,string Description,string ImageFile,decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool isSuccess);

    internal class UpdateProductQueryHandler
        (IDocumentSession session, ILogger<UpdateProductQueryHandler> logger)
        : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (product is null)
            {
                logger.LogWarning("Product with id {id} not found", command.id);
                throw new ProductNotFoundException(command.id);
            }
            product.Name = command.Name;
            product.Category = command.Categories;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }   

    }
}
