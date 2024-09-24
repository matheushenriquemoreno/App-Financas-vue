using Domain.Entity;

namespace Domain.Relatorios.Entity;

public class AcumuladoMensal
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public List<Despesa> Despesas { get; set; }
    public List<Investimento> Investimentos { get; set; }
    public List<Rendimento> Rendimentos { get; set; }

    public decimal ValorDeRendimento
    {
        get
        {
            return Rendimentos.Sum(x => x.Valor);
        }
    }
    public decimal ValorDeInvestimentos
    {
        get
        {
            return Investimentos.Sum(x => x.Valor);
        }
    }
    public decimal ValorDeDespesas
    {
        get
        {
            return Despesas.Sum(x => x.Valor);
        }
    }
    public decimal ValorRestanteMes
    {
        get
        {
            return ValorDeRendimento - ValorDeInvestimentos - ValorDeDespesas;
        }
    }
}
