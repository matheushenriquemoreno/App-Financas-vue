using System.Linq.Expressions;
using Domain.Entity;
using Domain.Repository;
using Infra.Data.Mongo.RepositoryBase;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infra.Data.Mongo.Repositorys;

public class CategoriaRepository : RepositoryMongoBase<Categoria>, ICategoriaRepository
{
    public IQueryable<Categoria> GetCategorias()
    {
        return _entityCollection.AsQueryable();
    }

    public Task<List<Categoria>> GetCategoriasOnde(Expression<Func<Categoria, bool>> filtro)
    {
        return _entityCollection.Find(filtro).ToListAsync();
    }

    public override string GetCollectionName()
        => nameof(Categoria);
}
