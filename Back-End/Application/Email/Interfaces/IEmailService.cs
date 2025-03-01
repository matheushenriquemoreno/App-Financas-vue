using Domain.Login.Entity;

namespace Application.Email.Interfaces
{
    public interface IEmailService
    {
        Task<bool> EnviarEmailLogin(bool primeiroLogin, string email, CodigoLogin codigo);
    }
}
