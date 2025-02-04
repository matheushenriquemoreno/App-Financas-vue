using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Configs
{
    public class DomainExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public DomainExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not DomainValidatorException notFoundException)
            {
                return false;
            }

            var erros = (exception as DomainValidatorException).Errors;

            var apiError = ApiResultError.Create(erros);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response
                .WriteAsJsonAsync(apiError, cancellationToken);

            _logger.LogError("DomainValidatorException: {Message}", string.Join(",", erros));

            return true;

        }
    }
}
