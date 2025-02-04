using System.ComponentModel.DataAnnotations;
using Application.Shared.Interfaces.DTOs;
using Domain.Entity;
using Domain.Enum;

namespace Application.DTOs;

public class CreateUpdateCategoriaDTO : ICreateDTO<Categoria>
{
    public string? Id { get; set; }
    public string Nome { get; set; }
    [EnumDataType(typeof(TipoCategoria), ErrorMessage = "Valor informado para Tipo da categoria invalido!")]
    public TipoCategoria Tipo { get; set; }

    public Categoria Mapear()
    {
        return new Categoria(this.Nome, this.Tipo);
    }
}

