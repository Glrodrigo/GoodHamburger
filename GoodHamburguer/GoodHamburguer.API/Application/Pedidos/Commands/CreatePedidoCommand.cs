using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Commands;

public record CreatePedidoCommand(
    List<int> ProdutoIds) : IRequest<Result<int>>;
