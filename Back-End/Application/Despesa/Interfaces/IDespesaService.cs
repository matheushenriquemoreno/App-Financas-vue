using Application.DTOs;
using Application.Shared.Interfaces.Service;
using Domain.Entity;

namespace Application.Interfaces;

public interface IDespesaService : IServiceBase<Despesa, CreateDespesaDTO, UpdateDespesaDTO, ResultDespesaDTO>
{
    Task<List<ResultDespesaDTO>> ObterRendimentoMes(int mes, int ano);
}
