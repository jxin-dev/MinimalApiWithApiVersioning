using Carter;
using MediatR;

namespace WebApi.Features.Products.GetProduct
{
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductQuery(id);
                var result = await sender.Send(query);

                return Results.Ok(result);
            })
                .WithName("GetProduct")
                .WithTags("Products")
                .MapToApiVersion(1);


            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductQuery(id);
                var result = await sender.Send(query);

                return Results.Ok(result);
            })
                .WithTags("Products")
                .MapToApiVersion(2);
        }

    }
}
