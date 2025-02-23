using System.Linq.Expressions;
using Domain.Entity;
using Domain.Enum;

namespace Domain.Repository;

public interface ICategoriaRepository : IRepositoryBase<Categoria>
{
    bool CategoriaJaExiste(string nome, TipoCategoria tipo);
    IQueryable<Categoria> GetCategorias();
    Task<List<Categoria>> GetCategoriasOnde(Expression<Func<Categoria, bool>> filtro);
}
