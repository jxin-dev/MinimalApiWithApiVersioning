using FluentValidation;

namespace WebApi.Features.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.InStock).NotEmpty();

        }
    }
}
