namespace NoticiasAPI.Models;

public class Noticia
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Contenido { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public DateTime FechaPublicacion { get; set; }
}