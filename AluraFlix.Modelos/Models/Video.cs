using System.ComponentModel.DataAnnotations;

namespace AluraFlix.Modelos.Models;

public class Video
{
    public int Id { get; private set; }
    public virtual int CategoriaId { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Url { get; set; }
}
