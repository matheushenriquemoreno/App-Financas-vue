using Domain.Exceptions;
using Domain.Validator;

namespace Domain.Entity;

public abstract class Transacao : EntityBase
{
    public int Ano { get; set; }
    public int Mes {  get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public Categoria Categoria { get; set; }
    public DateTime DataCriacao { get; set; }

    protected Transacao(string id, int ano, int mes, string descricao, decimal valor, Categoria categoria)
    {
        Id = id;
        Ano = ano;
        Mes = mes;
        Descricao = descricao;
        Valor = valor;
        Categoria = categoria;
        DataCriacao = DateTime.Now;
        this.ValidarDados();
    }

    private void ValidarDados()
    {
        var validator = DomainValidator.Intanciar();

        validator.Validar(() => this.Valor < 0, "Não se pode adicionar uma transação negativa.");
        validator.Validar(() => string.IsNullOrEmpty(this.Id), "Necessario um identificador para a transação.");

        validator.LancarExceptionSePossuiErro();
    }
}
