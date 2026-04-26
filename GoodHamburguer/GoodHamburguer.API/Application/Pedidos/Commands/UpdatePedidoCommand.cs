using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Commands;

public record UpdatePedidoCommand(int Id, List<int> ProdutoIds) 
    : IRequest<Result<PedidoResponse?>>;
