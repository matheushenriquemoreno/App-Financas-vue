using System.ComponentModel.DataAnnotations;
using Application.Shared.Interfaces.DTOs;
using Domain.Entity;
using Domain.Enum;

namespace Application.DTOs;

public class CreateCategoriaDTO : ICreateDTO<Categoria>
{
    public string Nome { get; set; }

    [Required(ErrorMessage = "Campo Tipo e obrigatorio!")]
    public TipoCategoria? Tipo { get; set; }

    public Categoria Mapear()
    {
        return new Categoria(this.Nome, this.Tipo.Value);
    }
}

