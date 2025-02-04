using Application.DTOs;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controlles;

public static class CategoriaRoutes
{
    public static RouteGroupBuilder MapRotasCategoria(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:length(24)}", async (string id, ICategoriaService service) =>
        {
            var result = await service.ObterPeloID(id);

            return result.MapResult();
        }).WithOpenApi();

        group.MapGet("/", async ([FromQuery] TipoCategoria tipoCategoria, ICategoriaService service) =>
        {
            var result = await service.ObterCategoriaPeloTipo(tipoCategoria);

            return result.MapResult();
        }).WithOpenApi();

        group.MapPost("/", async (CreateUpdateCategoriaDTO categoriadto, ICategoriaService service) =>
        {
            var result = await service.Adicionar(categoriadto);

            return result.MapResultCreated();
        }).WithOpenApi();

        group.MapPut("/", async ([FromBody] CreateUpdateCategoriaDTO categoriadto, ICategoriaService service) =>
        {
            var result = await service.Atualizar(categoriadto);

            return result.MapResult();
        }).WithOpenApi();

        group.MapDelete("/{id:length(24)}", async (string id, ICategoriaService service) =>
        {
            var result = await service.Excluir(id);

            return result.MapResult();
        }).WithOpenApi();

        return group;
    }

}
