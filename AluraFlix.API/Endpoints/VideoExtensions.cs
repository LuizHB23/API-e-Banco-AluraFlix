using System.Text.Json;
using AluraFlix.API.Endpoints.Response;
using AluraFlix.Banco;
using AluraFlix.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AluraFlix.API.Endpoints;

public static class VideoExtensions
{
    public static void AddEndpointsVideo(this WebApplication app)
    {
        app.MapGet("/videos", async ([FromServices] AluraflixDal<Video> dal) => 
        {
            var listaVideos = await dal.ListarAsync();

            if(listaVideos is null) 
            {
                Results.InternalServerError("Hourve um erro no servidor");
            }

            return Results.Ok(listaVideos);
        });

        app.MapGet("/videos/{id}", ([FromServices] AluraflixDal<Video> dal, int id) =>
        {
            var video = dal.RecuperarPor(v => v.Id == id);

            if(video is null) return Results.NotFound("Vídeo não encontrado");

            return Results.Ok(video);
        });

        app.MapPost("/videos", async ([FromBody] Video video, [FromServices] AluraflixDal<Video> dal) =>
        {   
            if(video.Titulo.Trim().IsNullOrEmpty() || video.Descricao.Trim().IsNullOrEmpty()) 
            {
                return Results.BadRequest("Você deve preencher corretamente o que foi requerido");
            }

            if(!VerificaUrl(video))
            {
                
                return Results.BadRequest("A url é inválida");
            }

            video.Titulo = video.Titulo.Trim();
            video.Descricao = video.Descricao.Trim();
            video.Url = video.Url.Trim();

            // await dal.AdicionarAsync(video);
            return Results.Json(video);
        });

        app.MapPut("/videos/{id}", async ([FromBody] Video video, [FromServices] AluraflixDal<Video> dal, int id) => 
        {
            Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

            if(videoRecuperado is null) 
            {
                return Results.BadRequest("O vídeo não existe");
            }

            if(!video.Titulo.IsNullOrEmpty())
            {
                videoRecuperado!.Titulo = video.Titulo;
            }

            if(!video.Descricao.IsNullOrEmpty())
            {
                videoRecuperado!.Descricao = video.Descricao;
            }

            if(VerificaUrl(video))
            {
                
                videoRecuperado!.Url = video.Url;
            }

            await dal.AtualizarAsync(videoRecuperado!);
            return Results.Ok("Vídeo atualizado com sucesso");
        });

        app.MapPatch("/videos/{id}", async ([FromBody] object objeto, [FromServices] AluraflixDal<Video> dal, int id) => 
        {
            Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

            if(videoRecuperado is null) 
            {
                return Results.BadRequest("O vídeo não existe");
            }

            var json = JsonSerializer.Serialize(objeto);
            var video = JsonSerializer.Deserialize<VideoResponse>(json);
            int contagem = 0;

            if(!video.titulo.IsNullOrEmpty())
            {
                videoRecuperado!.Titulo = video.titulo;
                contagem++;
            }

            if(!video.descricao.IsNullOrEmpty())
            {
                videoRecuperado!.Descricao = video.descricao;
                contagem++;
            }

            if(VerificaUrl(video))
            {
                
                videoRecuperado!.Url = video.url;
                contagem++;
            }

            if(contagem == 0)
            {
                return Results.BadRequest("As informações passadas não geram um vídeo");
            }

            await dal.AtualizarAsync(videoRecuperado!);
            return Results.Ok("Vídeo atualizado com sucesso");
        });

        app.MapDelete("/videos/{id}", async ([FromServices] AluraflixDal<Video> dal, int id) => 
        {
            Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

            if(videoRecuperado is null) Results.BadRequest("O vídeo não existe");

            await dal.DeletarAsync(videoRecuperado!);
            return Results.NoContent();
        });
    }

    private static bool VerificaUrl(Video video)
    {
        return video.Url.Trim().StartsWith("https://") || video.Url.Trim().StartsWith("http://");
    }

    private static bool VerificaUrl(VideoResponse video)
    {
        return video.url.Trim().StartsWith("https://") || video.url.Trim().StartsWith("http://");
    }
}