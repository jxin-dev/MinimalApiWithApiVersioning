using MediatR;
using WebApi.Contracts.Responses;

namespace WebApi.Features.Products.CreateProduct
{
    public record CreateProductCommand(string ProductName, decimal Price, bool InStock) : IRequest<ProductResponse>;
}
