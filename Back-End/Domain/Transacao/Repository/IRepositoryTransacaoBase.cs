using Domain.Entity;
using Domain.Relatorios.Entity;
using SharedDomain.Entity;

namespace Domain.Repository;

public interface IRepositoryTransacaoBase<T> : IRepositoryBase<T> where T : Transacao
{
    Task<AcumuladoMensalReport> ObterAcumuladoMensal(int mes, int ano);   
}
