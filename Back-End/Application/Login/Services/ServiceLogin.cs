using Application.Email.Interfaces;
using Application.Login.DTOs;
using Domain.Entity;
using Domain.Event;
using Domain.Login.Entity;
using Domain.Login.Repository;
using Domain.Repository;

namespace Application.Login.Services;

public class ServiceLogin
{
    private readonly ICodigoLoginRepository _codigoLoginRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public ServiceLogin(ICodigoLoginRepository codigoLoginRepository, IEmailService emailService, IUsuarioRepository usuarioRepository)
    {
        _codigoLoginRepository = codigoLoginRepository;
        _emailService = emailService;
        _usuarioRepository = usuarioRepository;
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

                // criar uma classe que vai armazenar esses eventos, e logo apos a finalização via middlare ja fazer o disparo, ou deixar um 
                // job que executa a cada segundo fazendo os disparos
                usuarioCriadoEvent = new UsuarioCriadoEvent(usuario);
            }

            var codigo = CodigoLogin.Create(usuario.Email);

            await _codigoLoginRepository.Add(codigo);

            var emailEnviadoComSucesso = await _emailService.EnviarEmailLogin(usuarioCriadoEvent != null, usuario.Email, codigo);

            if (emailEnviadoComSucesso)
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
                return Result.Failure<ResultLoginDTO>(Error.NotFound("Codigo login informado inexistente!"));

            if (codigoLogin.EstaExpirado())
            {
                await _codigoLoginRepository.Delete(codigoLogin);
                return Result.Failure<ResultLoginDTO>(Error.NotFound("Codigo login informado expirado!"));
            }

            if (codigoLogin.CodigoValido(codigoLoginDTO.Email, codigoLoginDTO.Codigo))
            {
                var usuario = await _usuarioRepository.GetByEmail(codigoLogin.Email);
                var tokenAcess = CriarTokenUsuario(usuario);
                var result = new ResultLoginDTO(tokenAcess, usuario.Nome);

                await _codigoLoginRepository.Delete(codigoLogin);

                return Result.Success(result);
            }

            return Result.Failure<ResultLoginDTO>(Error.NotFound("Codigo login informado invalido!"));
        }
        catch (Exception ex)
        {
            return Result.Failure<ResultLoginDTO>(Error.Exception(ex));
        }
    }

    private string CriarTokenUsuario(Usuario usuario)
    {
        return usuario.Nome;
    }
}
