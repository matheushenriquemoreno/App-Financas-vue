using System.Net.Mail;
using System.Net;
using Application.Email.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infra.Email;

public class GoogleProvedor : IProvedorEmail
{
    private readonly ILogger<GoogleProvedor> logger;

    public GoogleProvedor(ILogger<GoogleProvedor> logger)
    {
        this.logger = logger;
    }

    public async Task<Result> EnviarEmail(string assunto, string conteudoHTML, string email)
    {
        try
        {
            logger.LogInformation("Provedor: {0} Enviando e-mail para {1}", nameof(GoogleProvedor), email);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("matheushenriquesoares35@gmail.com", "fnmj xwyq qbbn iwgv"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email, "finançasAPP"),
                Subject = assunto,
                Body = conteudoHTML,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);

            logger.LogInformation("Provedor: {0} e-mail enviado com sucesso! para: {1}", nameof(GoogleProvedor), email);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Provedor: {0} um erro ao Não foi possivel enviar e-mail para {1}", nameof(GoogleProvedor), email);
            return Result.Failure(Error.Exception(ex));
        }
    }
}
