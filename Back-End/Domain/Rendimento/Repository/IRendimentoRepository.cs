﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repository;

public interface IRendimentoRepository : IRepositoryBase<Rendimento>
{
    public Task<IEnumerable<Rendimento>> ObterPeloMes(int mes, int ano);
}
