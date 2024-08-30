using FluentValidation;
using MediatR;
using WebApi.Contracts.Responses;
using WebApi.Exceptions;

namespace WebApi.Features.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly ProductDbContext _dbContext;
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(ProductDbContext dbContext, IValidator<CreateProductCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            Product newProduct = new();
            newProduct.Id = Guid.NewGuid();
            newProduct.ProductName = request.ProductName;
            newProduct.Price = request.Price;
            newProduct.InStock = request.InStock;


            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();

            var response = new ProductResponse(
               newProduct.Id, newProduct.ProductName,
               newProduct.Price, newProduct.InStock);

            return response;
        }
    }
}
