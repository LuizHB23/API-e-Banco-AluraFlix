using System.Text.Json.Serialization;

namespace AluraFlix.Modelos.DTOs;
public record VideoDTO
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("titulo")]
    public string? Titulo { get; set; }

    [JsonPropertyName("descricao")]
    public string? Descricao { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("categoriaId")]
    public int? CategoriaId { get; set; }
}