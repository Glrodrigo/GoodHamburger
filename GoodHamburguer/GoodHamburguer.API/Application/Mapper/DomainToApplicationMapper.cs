using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.Shared;

namespace GoodHamburguer.API.Application.Mapper;

public static class DomainToApplicationMapper
{
    public static ProdutoResponse ToProdutoResponse(
        this Produto produto)
    {
        return new ProdutoResponse
        {
            Id = produto.Id,
            Categoria = produto.Categoria,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco
        };
    }

    public static PedidoResponse ToPedidoResponse(this Pedido pedido)
    {
        return new PedidoResponse
        {
            Id = pedido.Id,
            DataPedido = pedido.DataPedido,
            UpdatePedido = pedido.UpdatePedido,
            Subtotal = pedido.Subtotal,
            DescontoPercentual = pedido.DescontoPercentual,
            ValorDesconto = pedido.ValorDesconto,
            TotalFinal = pedido.TotalFinal,
            Itens = pedido.Itens.Select(i => new PedidoItemResponse
            {
                ProdutoId = i.ProdutoId,
                NomeProduto = i.Produto?.Nome ?? string.Empty,
                PrecoUnitario = i.PrecoUnitarioHistorico
            })
        };
    }
}
