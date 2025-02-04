using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Interfaces.DTOs;

public interface IResultDTO<TEntity, TResult>
{
    TResult Mapear(TEntity entity);
}
