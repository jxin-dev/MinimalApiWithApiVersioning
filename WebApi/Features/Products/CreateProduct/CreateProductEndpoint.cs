using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Requests;

namespace WebApi.Features.Products.CreateProduct
{
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("products", async ([FromBody] CreateProductRequest request, ISender sender) =>
            {
                var command = new CreateProductCommand(
                    request.ProductName, request.Price, request.InStock);

                var result = await sender.Send(command);

                return Results.CreatedAtRoute("GetProduct", new { id = result.Id }, result);
            })
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithTags("Products")
                .MapToApiVersion(1);
        }
    }
}
