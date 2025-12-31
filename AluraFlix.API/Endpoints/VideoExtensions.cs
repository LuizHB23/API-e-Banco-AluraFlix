using AluraFlix.Modelos.Response;
using AluraFlix.Banco;
using AluraFlix.Modelos.Models;
using Microsoft.AspNetCore.Mvc;
using AluraFlix.Modelos.DTOs;
using AluraFlix.API.Services;
using System.Data;

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
        }).WithTags("Videos").WithOpenApi();

        app.MapGet("/videos/{id}", ([FromServices] AluraflixDal<Video> dal, int id) =>
        {
            var video = dal.RecuperarPor(v => v.Id == id);

            if(video is null) return Results.NotFound("Vídeo não encontrado");

            VideoResponse videoResponse = new(video.Titulo, video.Descricao, video.Url, video.CategoriaId);

            return Results.Ok(videoResponse);
        }).WithTags("Videos").WithOpenApi();

         app.MapPost("/videos", async ([FromBody] VideoDTO videoDTO, [FromServices] AluraflixDal<Video> dalVideo, 
            [FromServices] AluraflixDal<CategoriaVideo> dalCategoria, [FromServices] ValidacaoServices services) =>
        {   
            switch (services.ValidaVideo(videoDTO))
            {
                case 1:
                    return Results.BadRequest("Você deve preencher corretamente o que foi requerido");
                case 2:
                    return Results.BadRequest("A url é inválida");
                default:
                    break;
            }

            Video video = new Video();
            
            video.Titulo = videoDTO.Titulo.Trim();
            video.Descricao = videoDTO.Descricao.Trim();
            video.Url = videoDTO.Url.Trim();

            //await dalVideo.AdicionarAsync(video);
            return Results.Json(video);
        }).WithTags("Videos").WithOpenApi();;

        app.MapPut("/videos/{id}", async ([FromBody] VideoDTO videoDTO, [FromServices] AluraflixDal<Video> dal,
                    [FromServices] ValidacaoServices services, int id) => 
        {
            Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

            if(videoRecuperado is null) 
            {
                return Results.BadRequest("O vídeo não existe");
            }

            if(services.ValidaDTO(videoDTO))
            {
                services.ValidacaoEAtualizaVideo(videoRecuperado, videoDTO);      
            }
            else
            {   
                return Results.BadRequest("As informações insuficientes");
            }

            //await dal.AtualizarAsync(videoRecuperado!);
            return Results.Ok("Vídeo atualizado com sucesso");
        }).WithTags("Videos").WithOpenApi();

        app.MapPatch("/videos/{id}", async ([FromBody] VideoDTO videoDTO, [FromServices] AluraflixDal<Video> dal, 
                      [FromServices] ValidacaoServices services, int id) => 
        {
            Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

            if(videoRecuperado is null)
            {
                return Results.BadRequest("O vídeo não existe");
            }

            try
            {
                services.ValidacaoEAtualizaVideo(videoRecuperado, videoDTO);      
            }
            catch (DataException e)
            {
                return Results.BadRequest("As informações passadas não geram um vídeo");
            }

            //await dal.AtualizarAsync(videoRecuperado!);
            return Results.Ok("Vídeo atualizado com sucesso");
        }).WithTags("Videos").WithOpenApi();;

        app.MapDelete("/videos/{id}", async ([FromServices] AluraflixDal<Video> dal, int id) => 
        {
            Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

            if(videoRecuperado is null)
            {
                return Results.NotFound("O vídeo não existe");
            }

            //await dal.DeletarAsync(videoRecuperado!);
            return Results.NoContent();
        }).WithTags("Videos").WithOpenApi();;
    }
}