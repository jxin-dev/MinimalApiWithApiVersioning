namespace WebApi.Contracts.Responses
{
    public record ProductResponse(Guid Id, string ProductName, decimal Price, bool InStock);
}
