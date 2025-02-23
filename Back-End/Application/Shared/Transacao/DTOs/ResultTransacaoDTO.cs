using Application.Shared.Transacao.DTOs;
using Domain.Entity;

namespace Application.Shared.DTOs;

public class ResultTransacaoDTO
{
    public string Id { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public string CategoriaNome { get; set; }

    public ReportAcumuladoDTO ReportAcumulado { get; set; }
}
