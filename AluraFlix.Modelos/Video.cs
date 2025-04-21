using System.ComponentModel.DataAnnotations;

namespace AluraFlix.Modelos;

public class Video
{
    public int Id { get; private set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Url { get; set; }
}
