namespace WebApi.Contracts.Requests
{
    public record CreateProductRequest(string ProductName, decimal Price, bool InStock);
}
