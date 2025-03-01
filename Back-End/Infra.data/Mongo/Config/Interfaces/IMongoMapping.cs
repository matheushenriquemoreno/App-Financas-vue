using MongoDB.Driver;

namespace Infra.Data.Mongo.Config.Interface;

public interface IMongoMapping
{
    void RegisterMap(IMongoClient mongoClient);
}
