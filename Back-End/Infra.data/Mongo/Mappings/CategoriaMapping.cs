using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infra.Data.Mongo.Config.Interface;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Mappings;

public class CategoriaMapping : IMongoMapping
{
    public void RegisterMap(IMongoClient mongoClient)
    {
        BsonClassMap.TryRegisterClassMap<Categoria>(classMap =>
        {
            classMap.AutoMap();
        });
    }
}
