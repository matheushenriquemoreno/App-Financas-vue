using Application.DTOs;
using Application.Shared.Interfaces.Service;
using Domain.Entity;

namespace Application.Interface;

public interface IRendimentoService : IServiceBase<Rendimento, CreateRendimentoDTO, UpdateRendimentoDTO, ResultRendimentoDTO>
{
    Task<List<ResultRendimentoDTO>> ObterRendimentoMes(int mes, int ano);
}
