using AluraFlix.Modelos.Enums;

namespace AluraFlix.Modelos.Models;

public class CategoriaVideo
{
    public int Id { get; private set; }
    public string Titulo { get; set; }
    public Cor Cor { get; set; }
}