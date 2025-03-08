using System.Reflection;
using Application.Email.Interfaces;
using Application.Login.Interfaces;
using Application.Login.Services;
using Infra.Autenticacao;
using Infra.Data.Mongo.Config;
using Infra.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IOC;

public static class Setup
{
    public static IServiceCollection RegistrarDependencias(this IServiceCollection services)
    {
        services.AddScoped<IServiceJWT, ServiceJWT>();
        services.AddScoped<IProvedorEmail, GoogleProvedor>();

        services.RegisterApplication(Assembly.Load("Application"));

        services.RegisterRepository(assemblyInterfaces: Assembly.Load("Domain"), assemblyImplementations: Assembly.Load("Infra"));

        services.ConfiguarMongoDB();

        return services;
    }
}
