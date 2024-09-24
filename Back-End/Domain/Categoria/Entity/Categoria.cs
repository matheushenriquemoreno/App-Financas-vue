namespace Domain.Entity;

public class Categoria : EntityBase
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public object Tipo { get; set; }
}
