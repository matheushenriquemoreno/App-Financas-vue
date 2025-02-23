﻿using Domain.Entity;
using Infra.Data.Mongo.Config.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infra.Data.Mongo.Mappings;

internal class TransacaoMapping : IMongoMapping
{
    public void RegisterMap()
    {
        BsonClassMap.TryRegisterClassMap<Transacao>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.UnmapMember(m => m.Categoria);
            cm.MapMember(m => m.CategoriaId).SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
    }
}
