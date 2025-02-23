using Domain.Exceptions;

namespace Domain.Validator;

public class DomainValidator
{
    private List<string> erros;

    public DomainValidator()
    {
        erros = new List<string>();
    }

    public static DomainValidator Intanciar()
     => new DomainValidator();


    /// <summary>
    /// função para validar registros, função espera que retorne true em caso de uma validação de erro e false para sucesso.
    /// </summary>
    /// <param name="validar">função que espera um retorno true ou false, true caso possui erro, e false caso não</param>
    /// <param name="mensagemErro"></param>
    public void Validar(Func<bool> validar, string mensagemErro)
    {
        if (validar())
            erros.Add(mensagemErro);
    }

    public bool PossuiErro()
    {
        return erros.Count > 0;
    }

    public List<string> ObterErros()
    {
        return erros;
    }

    public void LancarExceptionSePossuiErro()
    {
        if (PossuiErro())
            throw new DomainValidatorException(this.erros);
    }
}

