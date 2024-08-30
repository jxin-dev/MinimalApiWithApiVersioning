using FluentValidation;
using WebApi.Features.Products.CreateProduct;

namespace WebApi.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            //services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>(); //Or
            services.AddValidatorsFromAssemblyContaining(typeof(Program));
            return services;
        }
    }
}
