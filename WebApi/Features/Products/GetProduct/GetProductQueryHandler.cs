using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Contracts.Responses;
using WebApi.Exceptions;

namespace WebApi.Features.Products.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly ProductDbContext _dbContext;

        public GetProductQueryHandler(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == request.ProductId);
            if (product is null)
            {
                throw new NotFoundException($"The product with Id = {request.ProductId} was not found.");
            }

            var response = new ProductResponse(
                product.Id, product.ProductName,
                product.Price, product.InStock);

            return response;
        }
    }
}
