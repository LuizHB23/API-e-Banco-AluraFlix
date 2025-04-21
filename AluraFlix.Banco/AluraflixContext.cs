using AluraFlix.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Banco;

public class AluraflixContext : DbContext
{
    private readonly string connectionString = "Server=localhost; Database=Aluraflix; Integrated Security=SSPI; TrustServerCertificate=True";

    public DbSet<Video> Videos { get; set; }

    public AluraflixContext() : base() { }

    public AluraflixContext(DbContextOptions<AluraflixContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
    }
}