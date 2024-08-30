using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Carter;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebApi.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static IServiceCollection AddApiVersioningWithSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });

           
            services.ConfigureOptions<ConfigureSwaggerGenOptions>();

            services.AddCarter();

            return services;
        }

        public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider _provider;

            public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
            {
                _provider = provider;
            }

            public void Configure(string? name, SwaggerGenOptions options)
            {
                Configure(options);
            }

            public void Configure(SwaggerGenOptions options)
            {
                foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
                {
                    var openApiInfo = new OpenApiInfo
                    {
                        Title = $"Sample.Api v{description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                    };

                    options.SwaggerDoc(description.GroupName, openApiInfo);
                }

            }
        }

        public static IApplicationBuilder UseConfiguredApiVersioning(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                ApiVersionSet apiVersionSet = endpoints.NewApiVersionSet()
                   .HasApiVersion(new ApiVersion(1))
                   .HasApiVersion(new ApiVersion(2))
                   .HasApiVersion(new ApiVersion(3))
                   .Build();

                RouteGroupBuilder versionGroup = endpoints.MapGroup("api/v{version:apiVersion}")
                   .WithApiVersionSet(apiVersionSet);

                versionGroup.MapCarter();


                if (env.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        var descriptions = endpoints.DescribeApiVersions();
                        foreach (var description in descriptions)
                        {
                            var url = $"/swagger/{description.GroupName}/swagger.json";
                            var name = description.GroupName.ToUpperInvariant();
                            options.SwaggerEndpoint(url, name);
                        }
                    });
                }

            });

            return app;
        }
    }
}
