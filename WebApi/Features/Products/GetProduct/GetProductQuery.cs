using MediatR;
using WebApi.Contracts.Responses;

namespace WebApi.Features.Products.GetProduct
{
    public record GetProductQuery(Guid ProductId) : IRequest<ProductResponse>;
}
