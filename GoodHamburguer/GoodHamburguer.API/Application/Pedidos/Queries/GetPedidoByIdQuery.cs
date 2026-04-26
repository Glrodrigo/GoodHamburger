using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Queries;

public record GetPedidoByIdQuery(int Id) 
    : IRequest<Result<PedidoResponse?>>;
