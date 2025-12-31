using AluraFlix.Modelos.Models;
using AluraFlix.Modelos.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AluraFlix.Banco.Mapeamento;

public class CategoriaTypeConfiguration : IEntityTypeConfiguration<CategoriaVideo>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CategoriaVideo> builder)
    {
        builder
            .Property(c => c.Cor)
            .HasConversion( 
                fromObj => fromObj.ToString(),
                fromDb => (Cor)Enum.Parse(typeof(Cor), fromDb)
            );
    }
}
