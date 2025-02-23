using Domain.Exceptions;
using Domain.Validator;

namespace Domain.Entity;

public abstract class Transacao : EntityBase
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public string CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public DateTime DataCriacao { get; set; }

    protected Transacao() { }

    protected Transacao(int ano, int mes, string descricao, decimal valor, Categoria categoria)
    {
        Ano = ano;
        Mes = mes;
        Descricao = string.IsNullOrEmpty(descricao) ? categoria.Nome : descricao;
        Valor = valor;

        if (CategoriaEhValida(categoria))
            CategoriaId = categoria.Id;
        else
            throw new DomainValidatorException("Categoria informada invalida para vinculo.");

        Categoria = categoria;

        DataCriacao = DateTime.Now;
        this.ValidarDados();
    }

    protected abstract bool CategoriaEhValida(Categoria categoria);

    private void ValidarDados()
    {
        var validator = DomainValidator.Intanciar();

        validator.Validar(() => this.Valor < 0, "Não se pode adicionar uma transação negativa.");
        validator.Validar(() => this.Ano < DateTime.Now.Year - 5, "Ano informado invalido.");
        validator.Validar(() => this.Mes < 1 || this.Mes > 12, "Mês informado invalido!");

        validator.LancarExceptionSePossuiErro();
    }
}
