using Newtonsoft.Json.Serialization;
using AluraFlix.Modelos;
using System.Text.Json;
using Newtonsoft.Json;
using AluraFlix.API.Videos;
using AluraFlix.Banco;

AluraflixDal<Video> dal = new AluraflixDal<Video>(new AluraflixContext());

using (StreamReader streamReader = new StreamReader("db.json"))
{
    var json = streamReader.ReadToEnd();
    var listaVideos = JsonConvert.DeserializeObject<listaVideos>(json);

    foreach(var video in listaVideos.videos)
    {
        Video novoVideo = new Video() { Titulo = video.Titulo, Descricao = video.Descricao, Url = video.Url};
        await dal.AdicionarAsync(novoVideo);
    }

}
