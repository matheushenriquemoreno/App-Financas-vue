using Domain.Entity;
using Infra.Data.Mongo.Config.Interface;
using MongoDB.Bson.Serialization;

namespace Infra.Data.Mongo.Mappings;

internal class RendimentoMapping : IMongoMapping
{
    public void RegisterMap()
    {
        BsonClassMap.TryRegisterClassMap<Rendimento>(cm =>
        {
            cm.AutoMap();
        });
    }
}
