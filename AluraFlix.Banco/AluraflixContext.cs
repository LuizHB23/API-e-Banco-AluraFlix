using AluraFlix.Modelos.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Banco;

public class AluraflixContext : DbContext
{
    private readonly string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Aluraflix;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

    public DbSet<Video> Videos { get; set; }
    public DbSet<CategoriaVideo> CategoriaVideos { get; set; }

    public AluraflixContext() : base() { }

    public AluraflixContext(DbContextOptions<AluraflixContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AluraflixContext).Assembly);
    }
}