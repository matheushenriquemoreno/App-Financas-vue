using Domain.Enum;
using Domain.Validator;

namespace Domain.Entity;

public class Categoria : EntityBase
{
    public string Nome { get; set; }
    public TipoCategoria Tipo { get; set; }

    public Categoria()
    {
    }

    public Categoria(string nome, TipoCategoria tipo)
    {
        Nome = nome;
        Tipo = tipo;
        ValidarDados();
    }

    private void ValidarDados()
    {
        var validator = DomainValidator.Intanciar();

        validator.Validar(() => string.IsNullOrEmpty(this.Nome), "Nome categoria obrigatorio!");
        validator.Validar(() => !System.Enum.IsDefined(typeof(TipoCategoria), this.Tipo), "Categoria informada invalida!");

        validator.LancarExceptionSePossuiErro();
    }
}
