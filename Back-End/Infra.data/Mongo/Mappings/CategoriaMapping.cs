using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infra.Data.Mongo.Mappings.Interface;
using MongoDB.Bson.Serialization;

namespace Infra.Data.Mongo.Mappings;

public class CategoriaMapping : IMongoMapping
{
    public void RegisterMap()
    {
        BsonClassMap.TryRegisterClassMap<Categoria>(classMap =>
        {
            classMap.AutoMap();
        });
    }
}
