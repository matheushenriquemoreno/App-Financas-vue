using Application.DTOs;
using Application.Shared.Interfaces.Service;
using Domain.Entity;

namespace Application.Interface;

public interface IInvestimentoService : IServiceBase<Investimento, CreateInvestimentoDTO, UpdateInvestimentoDTO, ResultInvestimentoDTO>
{
    Task<List<ResultInvestimentoDTO>> ObterRendimentoMes(int mes, int ano);
}
