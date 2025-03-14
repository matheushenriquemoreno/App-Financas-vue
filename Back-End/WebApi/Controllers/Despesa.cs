﻿using Application.DTOs;
using Application.Interface;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public static class Despesa
{
    public static RouteGroupBuilder MapDespesaEndpoints(this IEndpointRouteBuilder enpointRouteBuilder)
    {
        var group = enpointRouteBuilder.MapGroup("/api/Despesas");

        group.MapGet("/{id:length(24)}", async (string id, IDespesaService service) =>
        {
            var result = await service.ObterPeloID(id);

            return result.MapResult();
        });

        group.MapGet("/", async ([FromQuery] int mes, [FromQuery] int ano, IDespesaService service) =>
        {
            var result = await service.ObterRendimentoMes(mes, ano);

            return Results.Ok(result);
        });

        group.MapPost("/", async (CreateDespesaDTO categoriadto, IDespesaService service) =>
        {
            var result = await service.Adicionar(categoriadto);

            return result.MapResultCreated();
        });

        group.MapPut("/", async (UpdateDespesaDTO categoriadto, IDespesaService service) =>
        {
            var result = await service.Atualizar(categoriadto);

            return result.MapResult();
        });

        group.MapDelete("/{id:length(24)}", async (string id, IDespesaService service) =>
        {
            var result = await service.Excluir(id);

            return result.MapResult();
        });

        return group;
    }
}
