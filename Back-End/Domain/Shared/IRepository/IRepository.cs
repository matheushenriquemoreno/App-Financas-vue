using Domain.Entity;

namespace Domain;

public interface IRepository<T> where T : EntityBase
{
    T Add(T entity);
    T Update(T entity);
    void Delete(T entity);
    T GetByID(string id);
}

