using Domain.Entity;

namespace Application.Shared.Interfaces.Service;

public interface IServiceBase<T> where T : EntityBase
{
    Task<Result<T>> Adicionar(T entity);
    Task<Result<T>> Atualizar(T entity);
    Result Excluir(T entity);
    Task<Result<T>> ObterPeloID(string id);
}
