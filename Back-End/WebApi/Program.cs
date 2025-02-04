using Microsoft.AspNetCore.Mvc;
using Infrastructure.IOC;
using WebApi.Controlles;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Microsoft.Extensions.Logging.Console;
using WebApi.Configs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole(x => x.FormatterName = ConsoleFormatterNames.Json);

builder.Services.AddOpenApi();

builder.Services.RegistrarDepencias();

builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapGroup("/api/Categorias")
      .MapRotasCategoria()
      .WithTags("Categorias");

app.Run();
