using System.Reflection;
using Infra.Data.Mongo.Mappings.Interface;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infra.Data.Mongo;

public static class MongoConfig
{
    public static void MappingAllClassMongo(this IServiceCollection services)
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

    public static IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var mongoClient = new MongoClient(MongoDBSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(MongoDBSettings.DataBaseName);
        return mongoDatabase.GetCollection<T>(collectionName);
    }
}
