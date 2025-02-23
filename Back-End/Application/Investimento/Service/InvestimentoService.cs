
using Application.DTOs;
using Application.Interface;
using Application.Shared.Interfaces.DTOs;
using Domain.Entity;
using Domain.Relatorios.AcumuladoMensal;
using Domain.Relatorios.Entity;
using Domain.Repository;
using Mapster;

namespace Application.Service;

public class InvestimentoService : IInvestimentoService
{
    private readonly IInvestimentoRepository _investimentoRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IAcumuladoMensalReportRepository _acumuladoMensalReportRepository;

    public InvestimentoService(IAcumuladoMensalReportRepository acumuladoMensalReportRepository, IInvestimentoRepository investimentoRepository, ICategoriaRepository categoriaRepository)
    {
        _acumuladoMensalReportRepository = acumuladoMensalReportRepository;
        _investimentoRepository = investimentoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<Result<ResultInvestimentoDTO>> Adicionar(CreateInvestimentoDTO createDTO)
    {
        Categoria? categoria = await _categoriaRepository.GetByID(createDTO.CategoriaId);

        if (categoria == null)
            return Result.Failure<ResultInvestimentoDTO>(Error.NotFound("Categoria informada não existe!"));

        Investimento investimento = new(createDTO.Ano, createDTO.Mes, createDTO.Descricao, createDTO.Valor, categoria);

        await _investimentoRepository.Add(investimento);

        var reportAcumulado = await _acumuladoMensalReportRepository.Obter(investimento.Mes, investimento.Ano);

        ResultInvestimentoDTO rendimentoDTO = ObterResultInvestimentoDTO(investimento, reportAcumulado);

        return Result.Success(rendimentoDTO);
    }

    private ResultInvestimentoDTO ObterResultInvestimentoDTO(Investimento investimento, AcumuladoMensalReport? reportAcumulado = null)
    {
        var result = new ResultInvestimentoDTO()
        {
            Ano = investimento.Ano,
            Mes = investimento.Mes,
            CategoriaNome = investimento.Categoria?.Nome,
            Id = investimento.Id,
            Descricao = investimento.Descricao,
            Valor = investimento.Valor,
            ReportAcumulado = reportAcumulado
        };

        return result;
    }

    public async Task<Result<ResultInvestimentoDTO>> Atualizar(UpdateInvestimentoDTO updateDTO)
    {
        Investimento rendimento = await _investimentoRepository.GetByID(updateDTO.Id);

        if (rendimento == null)
            return Result.Failure<ResultInvestimentoDTO>(Error.NotFound("Rendimento informado não existe!"));

        rendimento.Descricao = updateDTO.Descricao;
        rendimento.CategoriaId = updateDTO.CategoriaId;
        rendimento.Valor = updateDTO.Valor;

        await _investimentoRepository.Update(rendimento);

        var reportAcumulado = await _acumuladoMensalReportRepository.Obter(rendimento.Mes, rendimento.Ano);

        ResultInvestimentoDTO rendimentoDTO = ObterResultInvestimentoDTO(rendimento, reportAcumulado);

        return Result.Success(rendimentoDTO);
    }

    public async Task<Result> Excluir(string id)
    {
        var investimento = await _investimentoRepository.GetByID(id);

        if (investimento == null)
            return Result.Failure(Error.NotFound("Investimento informado não existente"));

        await _investimentoRepository.Delete(investimento);

        return Result.Success();
    }

    public async Task<Result<ResultInvestimentoDTO>> ObterPeloID(string id)
    {
        var investimento = await _investimentoRepository.GetByID(id);

        if (investimento == null)
            return Result.Failure<ResultInvestimentoDTO>(Error.NotFound("Investimento informado não existente"));

        return Result.Success(ObterResultInvestimentoDTO(investimento));
    }

    public async Task<List<ResultInvestimentoDTO>> ObterRendimentoMes(int mes, int ano)
    {
        var despesas = await _investimentoRepository.ObterPeloMes(mes, ano);

        return despesas.Select(x => ObterResultInvestimentoDTO(x)).ToList();
    }
}
