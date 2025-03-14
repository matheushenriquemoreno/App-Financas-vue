﻿using System.Linq.Expressions;
using Domain;
using Infra.Data.Mongo.Config;
using MongoDB.Driver;
using SharedDomain.Entity;
using SharpCompress.Common;

namespace Infra.Data.Mongo.RepositoryBase;

public abstract class RepositoryMongoBase<T> : IRepositoryBase<T> where T : IEntityBase
{
    protected readonly IMongoCollection<T> _entityCollection;

    public RepositoryMongoBase(IMongoClient mongoClient)
    {
        _entityCollection = mongoClient.GetCollection<T>(MongoDBSettings.DataBaseName, this.GetCollectionName());
    }

    public abstract string GetCollectionName();

    public virtual async Task<T> Add(T entity)
    {
        await _entityCollection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task Delete(T entity) =>
        await _entityCollection.DeleteOneAsync(x => x.Id == entity.Id);

    public virtual async Task<T> GetByID(string id) =>
        await _entityCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public virtual async Task<T> Update(T entity)
    {
        await _entityCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> filtro)
    {
        return await _entityCollection.Find(filtro).ToListAsync();
    }
}
