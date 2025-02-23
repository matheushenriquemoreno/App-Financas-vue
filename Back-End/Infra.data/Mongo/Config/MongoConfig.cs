using System.Reflection;
using Infra.Data.Mongo.Config.Interface;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Config;

public static class MongoConfig
{
    private static void MappingAllClassMongo(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var classesMapeadoras = assembly.GetTypes()
            .Where(t => typeof(IMongoMapping).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var mapping in classesMapeadoras)
        {
            var instancia = Activator.CreateInstance(mapping) as IMongoMapping;
            instancia?.RegisterMap();
        }
    }

    public static void ConfiguarMongoDB(this IServiceCollection services)
    {
        services.MappingAllClassMongo();

        services.AddSingleton<IMongoClient>(s =>
            new MongoClient(MongoDBSettings.ConnectionString)
        );
    }
}
