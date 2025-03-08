using Application.Login.DTOs;
using Application.Login.Interfaces;

namespace WebApi.Controllers
{
    public static class Login
    {
        public static RouteGroupBuilder MapLoginEndpoints(this IEndpointRouteBuilder enpointRouteBuilder)
        {
            var group = enpointRouteBuilder.MapGroup("/api/login");

            group.MapPost("", async (ILoginService loginService, LoginDTO login) =>
            {
                var result = await loginService.Login(login);

                return result.MapResult();
            });

            group.MapPost("/validate-code", async (ILoginService loginService, CodigoLoginDTO codigoLoginDTO) =>
             {
                 var result = await loginService.CodigoEmailValido(codigoLoginDTO);

                 return result.MapResult();
             });

            return group;
        }
    }
}
