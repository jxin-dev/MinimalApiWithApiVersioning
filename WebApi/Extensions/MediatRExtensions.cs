namespace WebApi.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
            return services;
        }
    }
}
