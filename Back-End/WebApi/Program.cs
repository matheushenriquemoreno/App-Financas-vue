using Infrastructure.IOC;
using WebApi.Controlles;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Microsoft.Extensions.Logging.Console;
using WebApi.Configs;
using WebApi.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infra.Autenticacao;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTModel.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = true
        };
    });

builder.Services.AddAuthorization();

builder.Logging.ClearProviders();
builder.Logging.AddConsole(x => x.FormatterName = ConsoleFormatterNames.Json);

builder.Services.AddOpenApi();

builder.Services.RegistrarDependencias();

builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapLoginEndpoints()
    .WithTags("Login")
    .WithOpenApi();

app.MapCategoriaEndpoints()
      .WithTags("Categorias")
      .WithOpenApi()
      .RequireAuthorization();

app.MapRendimentoEndpoints()
      .WithTags("Rendimentos")
      .WithOpenApi()
      .RequireAuthorization();

app.MapDespesaEndpoints()
      .WithTags("Despesas")
      .WithOpenApi()
      .RequireAuthorization();

app.MapInvestimentoEndpoints()
    .WithTags("Investimentos")
    .WithOpenApi()
    .RequireAuthorization();

app.Run();
