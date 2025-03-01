using Domain.Login.Entity;
using Infra.Data.Mongo.Config;
using Infra.Data.Mongo.Config.Interface;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Mappings;

public class CodigoLoginMapping : IMongoMapping
{
    public void RegisterMap(IMongoClient mongoClient)
    {
        BsonClassMap.TryRegisterClassMap<CodigoLogin>(cm =>
        {
            cm.MapMember(x => x.Email).SetIsRequired(true);
            cm.MapMember(x => x.Codigo).SetIsRequired(true);
        });

        var database = mongoClient.GetDatabase(MongoDBSettings.DataBaseName);
        database.GetCollection<CodigoLogin>(nameof(CodigoLogin)).Indexes.List();
    }
}
