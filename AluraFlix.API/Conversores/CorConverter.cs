using AluraFlix.Modelos.Enums;

namespace AluraFlix.API.Conversores;

public class CorConverter
{
    public string ConverteCorParaString(Cor cor)
    {
        string nomeCor = cor.ToString();
        return nomeCor;
    }

    public bool ConvertStringParaCor(string nomeCor, ref Cor cor)
    {
        if(int.TryParse(nomeCor, out int obj)) return false;

        return Enum.TryParse(nomeCor, out cor);
    }
}