using GoodHamburguer.API.Domain.Pedidos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API.Infrastructure.Configs;

public class PedidoConfig : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DataPedido)
            .HasColumnName("dataPedido")
            .IsRequired();

        builder.Property(p => p.Subtotal)
            .HasPrecision(10, 2)
            .HasColumnName("subtotal")
            .IsRequired();

        builder.Property(p => p.DescontoPercentual)
            .HasPrecision(5, 2)
            .HasColumnName("descontoPercentual")
            .IsRequired();

        builder.Property(p => p.ValorDesconto)
            .HasPrecision(10, 2)
            .HasColumnName("valorDesconto")
            .IsRequired();

        builder.Property(p => p.TotalFinal)
            .HasPrecision(10, 2)
            .HasColumnName("totalFinal")
            .IsRequired();

        // Relacionamento 1:N (Um Pedido tem muitos Itens)
        builder.HasMany(p => p.Itens)
            .WithOne(i => i.Pedido)
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
