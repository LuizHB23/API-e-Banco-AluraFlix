using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Banco;

public class AluraflixDal<T> where T : class
{
    private readonly AluraflixContext context;

    public AluraflixDal(AluraflixContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<T>> ListarAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task AdicionarAsync(T objeto)
    {
        await context.Set<T>().AddAsync(objeto);
        await context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(T objeto)
    {
        context.Set<T>().Update(objeto);
        await context.SaveChangesAsync();
    }

    public async Task DeletarAsync(T objeto)
    {
        context.Set<T>().Remove(objeto);
        await context.SaveChangesAsync();
    }

    public T? RecuperarPor(Func<T,bool> condicao)
    {
        return context.Set<T>().FirstOrDefault<T>(condicao);
    }
}