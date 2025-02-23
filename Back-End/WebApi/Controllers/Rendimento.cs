using Application.DTOs;
using Application.Interface;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public static class Rendimento
    {
        public static RouteGroupBuilder MapRendimentoEndpoints(this IEndpointRouteBuilder enpointRouteBuilder)
        {
            var group = enpointRouteBuilder.MapGroup("/api/Rendimentos");

            group.MapGet("/{id:length(24)}", async (string id, IRendimentoService service) =>
            {
                var result = await service.ObterPeloID(id);

                return result.MapResult();
            });

            group.MapGet("/", async ([FromQuery] int mes, [FromQuery] int ano, IRendimentoService service) =>
            {
                var result = await service.ObterRendimentoMes(mes, ano);

                return Results.Ok(result);
            });

            group.MapPost("/", async (CreateRendimentoDTO categoriadto, IRendimentoService service) =>
            {
                var result = await service.Adicionar(categoriadto);

                return result.MapResultCreated();
            });

            group.MapPut("/", async (UpdateRendimentoDTO categoriadto, IRendimentoService service) =>
            {
                var result = await service.Atualizar(categoriadto);

                return result.MapResult();
            });

            group.MapDelete("/{id:length(24)}", async (string id, IRendimentoService service) =>
            {
                var result = await service.Excluir(id);

                return result.MapResult();
            });

            return group;
        }
    }
}
