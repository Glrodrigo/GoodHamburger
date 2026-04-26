using GoodHamburguer.API.Domain.Produtos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API.Infrastructure.Configs;

public class ProdutoConfig : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("nome");

        builder.Property(p => p.Categoria)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("categoria");

        builder.Property(p => p.Descricao)
            .HasMaxLength(255)
            .HasColumnName("descricao");

        builder.Property(p => p.Preco)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("preco");
    }
}
