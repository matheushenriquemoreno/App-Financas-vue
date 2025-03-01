using Domain.Entity;
using Infra.Data.Mongo.Config.Interface;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Mappings;

internal class InvestimentoMapping : IMongoMapping
{
    public void RegisterMap(IMongoClient mongoClient)
    {
        BsonClassMap.TryRegisterClassMap<Rendimento>(cm =>
        {
            cm.AutoMap();
        });
    }
}
