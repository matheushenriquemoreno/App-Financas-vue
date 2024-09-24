namespace Domain.Entity;

public class Investimento : Transacao
{
    public string BancoInvestimento { get; set; }

    public Investimento(string id, int ano, int mes, string descricao, decimal valor, Categoria categoria, string bancoInvestimento)
        : base(id, ano, mes, descricao, valor, categoria)
    {
        BancoInvestimento = bancoInvestimento;
    }
}
