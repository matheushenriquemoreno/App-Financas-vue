﻿using Application.DTOs;
using Application.Interfaces;
using Domain.Entity;
using Domain.Enum;
using Domain.Repository;

namespace Application.Implementacoes
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Result<ResultCategoriaDTO>> Adicionar(CreateUpdateCategoriaDTO entity)
        {
            var categoria = entity.Mapear();

            categoria = await _categoriaRepository.Add(categoria);

            return Result.Success(ResultCategoriaDTO.Mapear(categoria));
        }

        public async Task<Result<ResultCategoriaDTO>> Atualizar(CreateUpdateCategoriaDTO entity)
        {
            var categoria = await _categoriaRepository.GetByID(entity.Id);

            if (categoria is null)
                return Result.Failure<ResultCategoriaDTO>(Error.NotFound("Categoria informada não existe!"));

            categoria.Nome = entity.Nome;
            categoria.Tipo = entity.Tipo;

            await _categoriaRepository.Update(categoria);

            return Result.Success(ResultCategoriaDTO.Mapear(categoria));
        }

        public async Task<Result> Excluir(string id)
        {
            var categoria = await _categoriaRepository.GetByID(id);

            if (categoria is null)
                return Result.Failure(Error.NotFound("Categoria informada não existe!"));

            await _categoriaRepository.Delete(categoria);

            return Result.Success();
        }

        public async Task<Result<List<ResultCategoriaDTO>>> ObterCategoriaPeloTipo(TipoCategoria tipoCategoria)
        {
            var categorias = await _categoriaRepository.GetCategoriasOnde(x => x.Tipo == tipoCategoria);

            return Result.Success(categorias.Select(ResultCategoriaDTO.Mapear).ToList());
        }

        public async Task<Result<ResultCategoriaDTO>> ObterPeloID(string id)
        {
            var categoria = await _categoriaRepository.GetByID(id);

            if (categoria is null)
                return Result.Failure<ResultCategoriaDTO>(Error.NotFound("Categoria informada não existe!"));

            return Result.Success(ResultCategoriaDTO.Mapear(categoria));
        }
    }
}
