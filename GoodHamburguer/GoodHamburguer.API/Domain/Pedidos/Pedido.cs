using GoodHamburguer.API.Domain.Entidades;
using GoodHamburguer.API.Domain.Produtos;

namespace GoodHamburguer.API.Domain.Pedidos;

public sealed class Pedido : Entidade
{
    public Pedido() { }

    public Pedido(
        DateTime dataPedido,
        DateTime updatePedido)
    {
        DataPedido = dataPedido;
        UpdatePedido = updatePedido;
    }

    public decimal Subtotal { get; set; }
    public decimal DescontoPercentual { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal TotalFinal { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.Now;
    public DateTime UpdatePedido { get; set; } = DateTime.Now;

    private readonly List<PedidoItem> _itens = [];

    public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();

    public static Pedido Criar(
        DateTime dataPedido,
        DateTime updatePedido) 
    {
        return new Pedido(
            dataPedido,
            updatePedido);
    }

    public void CalcularTotais(IEnumerable<Produto> produtos)
    {
        Subtotal = produtos.Sum(p => p.Preco);
        DescontoPercentual = 0;

        bool temSanduiche = produtos.Any(p => p.Categoria == "Sanduiche");
        bool temBatata = produtos.Any(p => p.Nome.Contains("Batata"));
        bool temRefri = produtos.Any(p => p.Nome.Contains("Refrigerante"));

        bool descontado = false;

        // Regras de Desconto
        if (temSanduiche && temBatata && temRefri)
        {
            DescontoPercentual = 0.20m;
            descontado = true;
        }

        if (temSanduiche && temRefri && !descontado)
        {
            DescontoPercentual = 0.15m;
            descontado = true;
        }

        if (temSanduiche && temBatata && !descontado)
        {
            DescontoPercentual = 0.10m;
        }

        TotalFinal = Subtotal * (1 - DescontoPercentual);
        ValorDesconto = Subtotal * DescontoPercentual;
    }

    public void AdicionarItem(int produtoId, decimal preco)
    {
        var novoItem = PedidoItem.Criar(this.Id, produtoId, preco);
        _itens.Add(novoItem);
    }

    public void AtualizarItens(IEnumerable<Produto> novosProdutos)
    {
        _itens.Clear();

        foreach (var p in novosProdutos)
            AdicionarItem(p.Id, p.Preco);

        UpdatePedido = DateTime.Now;
    }
}
