using GoodHamburguer.API.Domain.Entidades;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.API.Domain.Produtos;
using Moq;
using FluentAssertions;

namespace GoodHamburguer.Tests;

public class PedidoTests
{
    private static Produto CriarProduto(int id, string nome, string categoria, decimal preco)
    {
        var p = new Produto(nome, categoria, string.Empty, preco);

        typeof(Entidade)
            .GetProperty("Id")!
            .SetValue(p, id);
        return p;
    }

    private readonly Produto _sanduiche = CriarProduto(1, "X Burger", "Sanduiche", 5.00m);
    private readonly Produto _batata = CriarProduto(2, "Batata frita", "Acompanhamento", 2.00m);
    private readonly Produto _refrigerante = CriarProduto(3, "Refrigerante", "Acompanhamento", 2.50m);

    [Fact]
    public void CalcularTotais_SanduicheApenas_SemDesconto()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);
        var produtos = new[] { _sanduiche };

        pedido.CalcularTotais(produtos);

        pedido.Subtotal.Should().Be(5.00m);
        pedido.DescontoPercentual.Should().Be(0);
        pedido.ValorDesconto.Should().Be(0);
        pedido.TotalFinal.Should().Be(5.00m);
    }

    [Fact]
    public void CalcularTotais_SanduicheMaisBatata_Desconto10Porcento()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);
        var produtos = new[] { _sanduiche, _batata };

        pedido.CalcularTotais(produtos);

        pedido.Subtotal.Should().Be(7.00m);
        pedido.DescontoPercentual.Should().Be(0.10m);
        pedido.ValorDesconto.Should().Be(0.70m);
        pedido.TotalFinal.Should().Be(6.30m);
    }

    [Fact]
    public void CalcularTotais_SanduicheMaisRefri_Desconto15Porcento()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);
        var produtos = new[] { _sanduiche, _refrigerante };

        pedido.CalcularTotais(produtos);

        pedido.Subtotal.Should().Be(7.50m);
        pedido.DescontoPercentual.Should().Be(0.15m);
        pedido.ValorDesconto.Should().BeApproximately(1.125m, 0.001m);
        pedido.TotalFinal.Should().BeApproximately(6.375m, 0.001m);
    }

    [Fact]
    public void CalcularTotais_ComboCompleto_Desconto20Porcento()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);
        var produtos = new[] { _sanduiche, _batata, _refrigerante };

        pedido.CalcularTotais(produtos);

        pedido.Subtotal.Should().Be(9.50m);
        pedido.DescontoPercentual.Should().Be(0.20m);
        pedido.ValorDesconto.Should().Be(1.90m);
        pedido.TotalFinal.Should().Be(7.60m);
    }


    [Fact]
    public void AdicionarItem_DeveIncrementarLista()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);

        pedido.AdicionarItem(_sanduiche.Id, _sanduiche.Preco);

        pedido.Itens.Should().HaveCount(1);
        pedido.Itens.First().PrecoUnitarioHistorico.Should().Be(5.00m);
    }


    [Fact]
    public void AtualizarItens_DeveSubstituirItensERecalcular()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);

        // Arrange inicial
        pedido.AdicionarItem(_sanduiche.Id, _sanduiche.Preco);
        pedido.CalcularTotais(new[] { _sanduiche });

        // Act
        var novosProdutos = new[] { _sanduiche, _batata, _refrigerante };
        pedido.AtualizarItens(novosProdutos);

        pedido.CalcularTotais(novosProdutos);

        // Assert
        pedido.Itens.Should().HaveCount(3);
        pedido.DescontoPercentual.Should().Be(0.20m);
        pedido.TotalFinal.Should().Be(7.60m);
    }
}
