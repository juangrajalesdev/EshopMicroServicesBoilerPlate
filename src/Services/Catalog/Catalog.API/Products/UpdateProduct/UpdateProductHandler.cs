using FluentValidation;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id,string Name, List<string> Categories,string Description,string ImageFile,decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool isSuccess);

    internal class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {

        public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
        {
            public UpdateProductCommandValidator()
            {
                RuleFor(command => command.id).NotEmpty().WithMessage("Product ID is required");

                RuleFor(command => command.Name)
                    .NotEmpty().WithMessage("Name is required")
                    .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

                RuleFor(command => command.Price)
                    .GreaterThan(0).WithMessage("Price must be greater than 0");
            }
        }


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
