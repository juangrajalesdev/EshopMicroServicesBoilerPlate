namespace Catalog.API.Exceptions
{
    public class ProductsNotFoundByCategoriesException : Exception
    {
        private List<string> category;

        public ProductsNotFoundByCategoriesException(string category) : base($"No products found for category {category}")
        {
        }
    }
}
