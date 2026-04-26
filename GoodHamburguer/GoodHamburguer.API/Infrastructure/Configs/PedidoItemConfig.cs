using GoodHamburguer.API.Domain.Pedidos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API.Infrastructure.Configs;

public class PedidoItemConfig : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.ToTable("PedidoItens");
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.PrecoUnitarioHistorico)
            .HasPrecision(10, 2)
            .HasColumnName("precoUnitarioHistorico")
            .IsRequired();

        // Relacionamento com Produto (N:1)
        builder.HasOne(pi => pi.Produto)
            .WithMany()
            .HasForeignKey(pi => pi.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
