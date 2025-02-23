namespace Domain.Entity;

public class Investimento : Transacao
{
    public Investimento(int ano, int mes, string descricao, decimal valor, Categoria categoria)
        : base(ano, mes, descricao, valor, categoria)
    {
    }

    protected override bool CategoriaEhValida(Categoria categoria)
    {
        if (categoria.Tipo == Enum.TipoCategoria.Investimento)
            return true;

        return false;
    }
}
