using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using WebApi.Extensions;
using static WebApi.Extensions.SwaggerGenOptionsExtensions;
using WebApi.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration();

builder.Services.AddMediatRConfiguration();

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioningWithSwaggerConfiguration();

var app = builder.Build();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseConfiguredApiVersioning(app.Environment);


app.UseHttpsRedirection();



app.Run();
