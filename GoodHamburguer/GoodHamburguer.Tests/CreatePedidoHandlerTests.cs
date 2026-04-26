using GoodHamburguer.API.Application.Pedidos.Commands;
using GoodHamburguer.API.Domain.Entidades;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.Shared;
using Moq;
using FluentAssertions;

namespace GoodHamburguer.Tests;

public class CreatePedidoHandlerTests
{
    private readonly Mock<IProdutoRepository> _produtoRepo = new();
    private readonly Mock<IPedidoRepository> _pedidoRepo = new();

    private CreatePedidoCommandHandler CriarHandler() =>
        new(_produtoRepo.Object, _pedidoRepo.Object);

    private static Produto CriarProduto(int id, string nome, string categoria, decimal preco)
    {
        var p = new Produto(nome, categoria, string.Empty, preco);
        typeof(Entidade).GetProperty("Id")!.SetValue(p, id);
        return p;
    }

    [Fact]
    public async Task Handle_ProdutoInvalido_RetornaFalha()
    {
        _produtoRepo
            .Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<int>>(), default))
            .ReturnsAsync(Result<IEnumerable<Produto>>.Ok([]));

        var handler = CriarHandler();
        var result = await handler.Handle(new CreatePedidoCommand([99]), default);

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("inválidos");
    }

    [Fact]
    public async Task Handle_CategoriaDuplicada_RetornaFalha()
    {
        var sanduiche1 = CriarProduto(1, "X Burger", "Sanduiche", 5.00m);
        var sanduiche2 = CriarProduto(2, "X Egg", "Sanduiche", 4.50m);

        _produtoRepo
            .Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<int>>(), default))
            .ReturnsAsync(Result<IEnumerable<Produto>>.Ok([sanduiche1, sanduiche2]));

        var result = await CriarHandler().Handle(new CreatePedidoCommand([1, 2]), default);

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("sanduíche");
    }

    [Fact]
    public async Task Handle_PedidoValido_CriaESalva()
    {
        var sanduiche = CriarProduto(1, "X Burger", "Sanduiche", 5.00m);
        var batata = CriarProduto(2, "Batata frita", "Acompanhamento", 2.00m);

        _produtoRepo
            .Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<int>>(), default))
            .ReturnsAsync(Result<IEnumerable<Produto>>.Ok([sanduiche, batata]));

        _pedidoRepo
            .Setup(r => r.AddAsync(It.IsAny<Pedido>()))
            .ReturnsAsync(Result<int>.Ok(1));

        var result = await CriarHandler().Handle(new CreatePedidoCommand([1, 2]), default);

        result.Success.Should().BeTrue();
        result.Data.Should().Be(1);

        _pedidoRepo.Verify(r => r.AddAsync(It.Is<Pedido>(p =>
            p.DescontoPercentual == 0.10m &&
            p.Itens.Count == 2
        )), Times.Once);
    }
}