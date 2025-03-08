using Domain.Login.Entity;
using Infra.Data.Mongo.Config;
using Infra.Data.Mongo.Config.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Mappings;

public class CodigoLoginMapping : IMongoMapping
{
    public void RegisterMap(IMongoClient mongoClient)
    {
        BsonClassMap.TryRegisterClassMap<CodigoLogin>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(x => x.Email).SetIsRequired(true);
            cm.MapMember(x => x.Codigo).SetIsRequired(true);
            cm.MapMember(x => x.DataCriacao).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.DateTime));
            cm.MapMember(x => x.DataExpiracao).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.DateTime));
        });

        //var database = mongoClient.GetDatabase(MongoDBSettings.DataBaseName);
        //database.GetCollection<CodigoLogin>(nameof(CodigoLogin)).Indexes.List();
    }
}
