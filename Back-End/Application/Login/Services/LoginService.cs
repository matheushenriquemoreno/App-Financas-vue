using Application.Email.Interfaces;
using Application.Login.DTOs;
using Application.Login.Interfaces;
using Domain.Entity;
using Domain.Event;
using Domain.Login.Entity;
using Domain.Login.Repository;
using Domain.Repository;

namespace Application.Login.Services;

public class LoginService : ILoginService
{
    private readonly ICodigoLoginRepository _codigoLoginRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioEmailService _emailService;
    private readonly IServiceJWT _serviceJWT;

    public LoginService(ICodigoLoginRepository codigoLoginRepository, IUsuarioEmailService emailService, IUsuarioRepository usuarioRepository, IServiceJWT serviceJWT)
    {
        _codigoLoginRepository = codigoLoginRepository;
        _emailService = emailService;
        _usuarioRepository = usuarioRepository;
        _serviceJWT = serviceJWT;
    }

    public async Task<Result> Login(LoginDTO login)
    {
        UsuarioCriadoEvent usuarioCriadoEvent = null;

        try
        {
            Usuario usuario = await _usuarioRepository.GetByEmail(login.Email);

            if (usuario == null)
            {
                usuario = new Usuario(login.Nome, login.Email);

                await _usuarioRepository.Add(usuario);

                usuarioCriadoEvent = new UsuarioCriadoEvent(usuario);
            }

            var codigo = CodigoLogin.Create(usuario.Email);

            await _codigoLoginRepository.Add(codigo);

            Result resultEmailEnviado = await _emailService.EnviarEmailParaLogin(usuarioCriadoEvent != null, usuario.Email, codigo);

            if (resultEmailEnviado.IsSucess)
            {
                return Result.Success();
            }
            else
            {
                await _codigoLoginRepository.Delete(codigo);
            }

            return Result.Failure();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Exception(ex));
        }
    }

    public async Task<Result<ResultLoginDTO>> CodigoEmailValido(CodigoLoginDTO codigoLoginDTO)
    {
        try
        {
            CodigoLogin codigoLogin = await _codigoLoginRepository.GetByCodigo(codigoLoginDTO.Codigo);

            if (codigoLogin == null)
                return Result.Failure<ResultLoginDTO>(Error.NotFound("Codigo informado invalido!"));

            if (codigoLogin.EstaExpirado())
            {
                await _codigoLoginRepository.DeleteExpirados(codigoLogin.Email);
                return Result.Failure<ResultLoginDTO>(Error.NotFound("Codigo informado invalido!"));
            }

            if (codigoLogin.CodigoValido(codigoLoginDTO.Email, codigoLoginDTO.Codigo))
            {
                var usuario = await _usuarioRepository.GetByEmail(codigoLogin.Email);
                var tokenAcess = _serviceJWT.CriarToken(usuario);
                var result = new ResultLoginDTO(tokenAcess, usuario.Nome);

                return Result.Success(result);
            }

            return Result.Failure<ResultLoginDTO>(Error.NotFound("Codigo informado invalido!"));
        }
        catch (Exception ex)
        {
            return Result.Failure<ResultLoginDTO>(Error.Exception(ex));
        }
    }
}
