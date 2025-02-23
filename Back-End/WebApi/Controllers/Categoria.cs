using Application.DTOs;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controlles;

public static class Categoria
{
    public static RouteGroupBuilder MapCategoriaEndpoints(this IEndpointRouteBuilder enpointRouteBuilder)
    {
        var group = enpointRouteBuilder.MapGroup("/api/Categorias");


        group.MapGet("/{id:length(24)}", async (string id, ICategoriaService service) =>
        {
            var result = await service.ObterPeloID(id);

            return result.MapResult();
        });

        group.MapGet("/", async ([FromQuery] TipoCategoria tipoCategoria, ICategoriaService service) =>
        {
            var result = await service.ObterCategoriaPeloTipo(tipoCategoria);

            return result.MapResult();
        });

        group.MapPost("/", async (CreateCategoriaDTO categoriadto, ICategoriaService service) =>
        {
            var result = await service.Adicionar(categoriadto);

            return result.MapResultCreated();
        });

        group.MapPut("/", async (UpdateCategoriaDTO categoriadto, ICategoriaService service) =>
        {
            var result = await service.Atualizar(categoriadto);

            return result.MapResult();
        });

        group.MapDelete("/{id:length(24)}", async (string id, ICategoriaService service) =>
        {
            var result = await service.Excluir(id);

            return result.MapResult();
        });

        return group;
    }

}
