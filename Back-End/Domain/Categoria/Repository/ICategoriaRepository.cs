using System.Linq.Expressions;
using Domain.Entity;

namespace Domain.Repository;

public interface ICategoriaRepository : IRepositoryBase<Categoria>
{
    IQueryable<Categoria> GetCategorias();
    Task<List<Categoria>> GetCategoriasOnde(Expression<Func<Categoria, bool>> filtro);
}
