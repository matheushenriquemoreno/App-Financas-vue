using Domain.Entity;
using SharedDomain.Entity;

namespace Application.Shared.Interfaces.DTOs;

public interface ICreateDTO<T> where T : IEntityBase
{
    T Mapear();
}
