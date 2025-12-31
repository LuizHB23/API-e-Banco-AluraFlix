using AluraFlix.API.Conversores;
using AluraFlix.Modelos.Response;
using AluraFlix.Banco;
using AluraFlix.Modelos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlix.API.Endpoints;

public static class CategoriaExtensions
{
    public static void AddEndpointsCategoria(this WebApplication app)
    {
        app.MapGet("/categorias", async ([FromServices] AluraflixDal<CategoriaVideo> dal, [FromServices] CorConverter converter) => 
        {
            var listaCategorias = await dal.ListarAsync();

            if(listaCategorias is null) 
            {
                Results.InternalServerError("Hourve um erro no servidor");
            }

            List<CategoriaVideoResponse> colecaoCategorias = new List<CategoriaVideoResponse>();
            string nomeCor;

            foreach(var categoria in listaCategorias!)
            {
                nomeCor = converter.ConverteCorParaString(categoria.Cor);
                CategoriaVideoResponse categoriaVideoResponse = new CategoriaVideoResponse(categoria.Id, categoria.Titulo, nomeCor);
                colecaoCategorias.Add(categoriaVideoResponse);
            }

            return Results.Ok(colecaoCategorias);
        }).WithTags("Categorias").WithOpenApi();

        app.MapGet("/categorias/{id}", ([FromServices] AluraflixDal<CategoriaVideo> dal, [FromServices] CorConverter converter, int id) =>
        {
            var categoria = dal.RecuperarPor(c => c.Id == id);

            if(categoria is null) return Results.NotFound("Categoria não encontrada");

            string nomeCor = converter.ConverteCorParaString(categoria.Cor);
            CategoriaVideoResponse categoriaVideoResponse = new CategoriaVideoResponse(categoria.Id, categoria.Titulo, nomeCor);

            return Results.Ok(categoriaVideoResponse);
        }).WithTags("Categorias").WithOpenApi();

        // app.MapPost("/categorias", async ([FromBody] CategoriaVideoRequest categoria, [FromServices] AluraflixDal<CategoriaVideo> dal, [FromServices] CorConverter converter) =>
        // {   
        //     if(categoria.titulo.Trim().IsNullOrEmpty()) 
        //     {
        //         return Results.BadRequest("Você deve preencher corretamente o que foi requerido");
        //     }

        //     Cor cor = new Cor();

        //     if(!converter.ConvertStringParaCor(categoria.cor, ref cor))
        //     {
        //         return Results.BadRequest("A cor é inválida");
        //     }

        //     CategoriaVideo novaCategoria = new CategoriaVideo() 
        //     {
        //         Titulo = categoria.titulo,
        //         Cor = cor
        //     };

        //     // await dal.AdicionarAsync(video);
        //     return Results.Json(converter.ConverteCorParaString(novaCategoria.Cor));
        // }).WithTags("Categorias").WithOpenApi();

        // app.MapPut("/categorias/{id}", async ([FromBody] CategoriaVideo categoria, [FromServices] AluraflixDal<CategoriaVideo> dal, int id) => 
        // {
        //     Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

        //     if(videoRecuperado is null) 
        //     {
        //         return Results.BadRequest("O vídeo não existe");
        //     }

        //     if(!video.Titulo.IsNullOrEmpty())
        //     {
        //         videoRecuperado!.Titulo = video.Titulo;
        //     }

        //     if(!video.Descricao.IsNullOrEmpty())
        //     {
        //         videoRecuperado!.Descricao = video.Descricao;
        //     }

        //     if(VerificaUrl(video))
        //     {
                
        //         videoRecuperado!.Url = video.Url;
        //     }

        //     await dal.AtualizarAsync(videoRecuperado!);
        //     return Results.Ok("Vídeo atualizado com sucesso");
        // }).WithTags("Categorias").WithOpenApi();

        // app.MapPatch("/categorias/{id}", async ([FromBody] object objeto, [FromServices] AluraflixDal<CategoriaVideo> dal, int id) => 
        // {
        //     Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

        //     if(videoRecuperado is null) 
        //     {
        //         return Results.BadRequest("O vídeo não existe");
        //     }

        //     var json = JsonSerializer.Serialize(objeto);
        //     var video = JsonSerializer.Deserialize<VideoResponse>(json);
        //     int contagem = 0;

        //     if(!video.titulo.IsNullOrEmpty())
        //     {
        //         videoRecuperado!.Titulo = video.titulo;
        //         contagem++;
        //     }

        //     if(!video.descricao.IsNullOrEmpty())
        //     {
        //         videoRecuperado!.Descricao = video.descricao;
        //         contagem++;
        //     }

        //     if(VerificaUrl(video))
        //     {
                
        //         videoRecuperado!.Url = video.url;
        //         contagem++;
        //     }

        //     if(contagem == 0)
        //     {
        //         return Results.BadRequest("As informações passadas não geram um vídeo");
        //     }

        //     await dal.AtualizarAsync(videoRecuperado!);
        //     return Results.Ok("Vídeo atualizado com sucesso");
        // });

        // app.MapDelete("/categorias/{id}", async ([FromServices] AluraflixDal<CategoriaVideo> dal, int id) => 
        // {
        //     Video? videoRecuperado = dal.RecuperarPor(v => v.Id == id);

        //     if(videoRecuperado is null) Results.BadRequest("O vídeo não existe");

        //     await dal.DeletarAsync(videoRecuperado!);
        //     return Results.NoContent();
        // }).WithTags("Categorias").WithOpenApi();
    }
}