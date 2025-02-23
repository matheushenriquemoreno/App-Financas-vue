using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repository;

public interface IInvestimentoRepository : IRepositoryBase<Investimento>
{
    public Task<IEnumerable<Investimento>> ObterPeloMes(int mes, int ano);
}
