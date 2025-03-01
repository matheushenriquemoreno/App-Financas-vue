using System.Reflection;
using Application.Login.Services;
using Infra.Data.Mongo.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IOC;

public static class Setup
{
    public static IServiceCollection RegistrarDependencias(this IServiceCollection services)
    {

        services.AddScoped<ServiceLogin>();

        services.RegisterApplication(Assembly.Load("Application"));

        services.RegisterRepository(assemblyInterfaces: Assembly.Load("Domain"), assemblyImplementations: Assembly.Load("Infra.Data"));

        services.ConfiguarMongoDB();

        return services;
    }
}
