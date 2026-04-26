using GoodHamburguer.API.Domain.Entidades;
using GoodHamburguer.API.Domain.Produtos;

namespace GoodHamburguer.API.Domain.Pedidos;

public sealed class PedidoItem : Entidade
{
    public PedidoItem() { }

    public PedidoItem(
        int pedidoId,
        int produtoId,
        decimal precoUnitarioHistorico)
    {
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        PrecoUnitarioHistorico = precoUnitarioHistorico;
    }

    public int PedidoId { get; set; }
    public int ProdutoId { get; set; }
    public decimal PrecoUnitarioHistorico { get; set; }

    public Pedido Pedido { get; set; } = null!;
    public Produto Produto { get; set; } = null!;

    public static PedidoItem Criar(
        int pedidoId,
        int produtoId,
        decimal precoUnitarioHistorico)
    {
        return new PedidoItem(
            pedidoId,
            produtoId,
            precoUnitarioHistorico);
    }
}
