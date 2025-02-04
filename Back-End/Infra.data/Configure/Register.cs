using System.Reflection;
using Infra.Data.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IOC;

public static class Register
{
    public static IServiceCollection RegistrarDepencias(this IServiceCollection services)
    {
        services.RegisterApplication(Assembly.Load("Application"));

        services.RegisterRepository(assemblyInterfaces: Assembly.Load("Domain"), assemblyImplementations: Assembly.Load("Infra.Data"));

        services.MappingAllClassMongo();

        return services;
    }
}
