using AluraFlix.Modelos.DTOs;
using AluraFlix.Modelos.Models;
using AluraFlix.Modelos.Response;
using System.Data;

namespace AluraFlix.API.Services;

public class ValidacaoServices
{
    public void ValidacaoEAtualizaVideo(Video video, VideoDTO videoDTO)
    {
        int contagem = 0;

        if(!VerificaSeEhVazioOuNulo(videoDTO.Titulo))
        {
            video!.Titulo = videoDTO.Titulo.Trim();
            contagem++;
        }

        if(!VerificaSeEhVazioOuNulo(videoDTO.Descricao))
        {
            video!.Descricao = videoDTO.Descricao.Trim();
            contagem++;
        }

        if(VerificaUrl(videoDTO.Url))
        {
            video!.Url = videoDTO.Url.Trim();
            contagem++;
        }

        if(videoDTO.CategoriaId != null)
        {
            video.CategoriaId = (int)videoDTO.CategoriaId;
            contagem++;
        }

        if(contagem == 0)
        {
            throw new DataException();
        }
    }

    public bool ValidaDTO(VideoDTO videoDTO)
    {
        if(VerificaSeEhVazioOuNulo(videoDTO.Titulo) && VerificaSeEhVazioOuNulo(videoDTO.Descricao) 
            && VerificaUrl(videoDTO.Url) && videoDTO.CategoriaId != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ValidaVideo(VideoDTO videoDTO)
    {
        // Se não forem válidos retorna erro 1
        if(VerificaSeEhVazioOuNulo(videoDTO.Titulo) || VerificaSeEhVazioOuNulo(videoDTO.Descricao)) 
        {
            return 1;
        }

        // Se não for válido retorna erro 2
        if(VerificaUrl(videoDTO.Url))
        {
            
            return 2;
        }

        return 0;
    }

    private bool VerificaSeEhVazioOuNulo(string texto) => string.IsNullOrWhiteSpace(texto);

    private bool VerificaUrl(string texto) => (texto == null ) ? false : texto.Trim().StartsWith("https://") || texto.Trim().StartsWith("http://");
}