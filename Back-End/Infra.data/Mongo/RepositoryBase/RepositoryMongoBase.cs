using Domain;
using Domain.Entity;
using MongoDB.Driver;
using SharedDomain.Entity;

namespace Infra.Data.Mongo.RepositoryBase;

public abstract class RepositoryMongoBase<T> : IRepositoryBase<T> where T : IEntityBase
{
    protected readonly IMongoCollection<T> _entityCollection;

    public RepositoryMongoBase()
    {
        _entityCollection = MongoConfig.GetCollection<T>(this.GetCollectionName());
    }
    public abstract string GetCollectionName();

    public async Task<T> Add(T entity)
    {
        await _entityCollection.InsertOneAsync(entity);
        return entity;
    }

    public async Task Delete(T entity) =>
        await _entityCollection.DeleteOneAsync(x => x.Id == entity.Id);

    public async Task<T> GetByID(string id) =>
        await _entityCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<T> Update(T entity)
    {
        await _entityCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return entity;
    }
}
