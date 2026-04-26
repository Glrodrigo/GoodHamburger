using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Commands;

public record DeletePedidoCommand(int Id) 
    : IRequest<Result<bool>>;
