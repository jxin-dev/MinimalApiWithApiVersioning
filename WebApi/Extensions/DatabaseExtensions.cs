using Microsoft.EntityFrameworkCore;
using WebApi.Features.Products;

namespace WebApi.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>(options => options.UseInMemoryDatabase("InMemoDb"));
            return services;
        }
    }
}
