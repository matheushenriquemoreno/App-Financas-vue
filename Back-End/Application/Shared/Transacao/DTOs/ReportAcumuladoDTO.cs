using Domain.Relatorios.Entity;

namespace Application.Shared.Transacao.DTOs;

public class ReportAcumuladoDTO
{
    public decimal ValorRendimento { get; set; }
    public decimal ValorInvestimentos { get; set; }
    public decimal ValorDespesas { get; set; }
    public decimal ValorFinal { get; set; }

    public static implicit operator ReportAcumuladoDTO?(AcumuladoMensalReport? reportAcumulado)
    {
        if (reportAcumulado == null)
            return default;

        return new ReportAcumuladoDTO()
        {
            ValorDespesas = reportAcumulado.ValorDespesas,
            ValorInvestimentos = reportAcumulado.ValorInvestimentos,
            ValorRendimento = reportAcumulado.ValorRendimento,
            ValorFinal = reportAcumulado.ValorFinal
        };
    }
}
