using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Domain.Entity;
using Domain.Enum;
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
    public CategoriaRepository(IMongoClient mongoClient) : base(mongoClient)
    {
    }
    public bool CategoriaJaExiste(string nome, TipoCategoria tipo)
    {
        var builder = Builders<Categoria>.Filter;

        FilterDefinition<Categoria> filtroNome = FiltrarNomeCategoriaIgnorandoCaseSensitive(nome, builder);
        FilterDefinition<Categoria> filtroCategoria = builder.Eq(x => x.Tipo, tipo);

        return _entityCollection.Find(builder.And(filtroNome, filtroCategoria))
            .Any();
    }

    private static FilterDefinition<Categoria> FiltrarNomeCategoriaIgnorandoCaseSensitive(string nome, FilterDefinitionBuilder<Categoria> builder)
    {
        return builder
            .Regex(x => x.Nome, new BsonRegularExpression($"^{Regex.Escape(nome)}$", "i"));
    }

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
