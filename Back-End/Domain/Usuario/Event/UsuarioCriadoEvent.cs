using Domain.Entity;

namespace Domain.Event;

public class UsuarioCriadoEvent
{
    public Usuario Usuario { get; set; }

    public UsuarioCriadoEvent(Usuario usuario)
    {
        Usuario = usuario;
    }
}
