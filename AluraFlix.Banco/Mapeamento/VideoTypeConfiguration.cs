using AluraFlix.Modelos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AluraFlix.Banco.Mapeamento;

public class VideoTypeConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Video> builder)
    {
        builder.Property(v => v.Id).HasColumnName("Id");
    }
}
