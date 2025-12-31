using AluraFlix.Modelos;
using System.Text.Json;
using AluraFlix.Videos.DB;
using AluraFlix.Banco;

AluraflixDal<Video> dal = new AluraflixDal<Video>(new AluraflixContext());

using (StreamReader streamReader = new StreamReader("db.json"))
{
    var json = streamReader.ReadToEnd();
    ListaVideos listaVideos = JsonSerializer.Deserialize<ListaVideos>(json);

    foreach(var video in listaVideos!.videos)
    {
        Video novoVideo = new Video() { Titulo = video.Titulo, CategoriaId=0, Descricao = video.Descricao, Url = video.Url};
        await dal.AdicionarAsync(novoVideo);
    }

}
