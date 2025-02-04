using Domain.Entity;
using Infra.Data.Mongo.Mappings.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Infra.Data.Mongo.Mappings;

public class EntityBaseMapping : IMongoMapping
{
    public void RegisterMap()
    {
        BsonClassMap.TryRegisterClassMap<EntityBase>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.Id)
              .SetIdGenerator(StringObjectIdGenerator.Instance)
              .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
    }
}
