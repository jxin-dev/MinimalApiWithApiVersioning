namespace WebApi.Features.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool InStock { get; set; }

    }
}
