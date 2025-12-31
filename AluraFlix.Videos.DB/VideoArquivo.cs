using System.Text.Json.Serialization;

namespace AluraFlix.Videos.DB;
internal class VideoArquivo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("titulo")]
    public string Titulo { get; set; }

    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}