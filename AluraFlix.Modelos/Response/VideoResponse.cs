namespace AluraFlix.Modelos.Response;

public record VideoResponse(string? Titulo, string? Descricao, string? Url, int? CategoriaId);