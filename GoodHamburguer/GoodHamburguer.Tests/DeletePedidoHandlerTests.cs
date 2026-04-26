using GoodHamburguer.API.Application.Pedidos.Commands;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.Shared;
using Moq;
using FluentAssertions;

namespace GoodHamburguer.Tests;

public class DeletePedidoHandlerTests
{
    private readonly Mock<IPedidoRepository> _repo = new();

    [Fact]
    public async Task Handle_PedidoNaoEncontrado_RetornaFalha()
    {
        _repo.Setup(r => r.GetByIdAsync(99, default))
             .ReturnsAsync(Result<Pedido?>.Ok(null));

        var handler = new DeletePedidoCommandHandler(_repo.Object);
        var result = await handler.Handle(new DeletePedidoCommand(99), default);

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task Handle_PedidoExistente_DeletaComSucesso()
    {
        var pedido = Pedido.Criar(DateTime.Now, DateTime.Now);

        _repo.Setup(r => r.GetByIdAsync(1, default))
             .ReturnsAsync(Result<Pedido?>.Ok(pedido));

        _repo.Setup(r => r.DeleteAsync(1, default))
             .ReturnsAsync(Result<bool>.Ok(true));

        var result = await new DeletePedidoCommandHandler(_repo.Object)
                            .Handle(new DeletePedidoCommand(1), default);

        result.Success.Should().BeTrue();
        _repo.Verify(r => r.DeleteAsync(1, default), Times.Once);
    }
}
