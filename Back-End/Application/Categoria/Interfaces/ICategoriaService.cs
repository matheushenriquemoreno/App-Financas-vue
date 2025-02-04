using Application.DTOs;
using Application.Shared.Interfaces.Service;
using Domain.Entity;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<Result<ResultCategoriaDTO>> Adicionar(CreateUpdateCategoriaDTO entity);
        Task<Result<ResultCategoriaDTO>> Atualizar(CreateUpdateCategoriaDTO entity);
        Task<Result> Excluir(string id);
        Task<Result<ResultCategoriaDTO>> ObterPeloID(string id);
        Task<Result<List<ResultCategoriaDTO>>> ObterCategoriaPeloTipo(TipoCategoria tipoCategoria);
    }
}
