using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Queries;

public record GetPedidoQuery()
    : IRequest<Result<IEnumerable<PedidoResponse>>>;
