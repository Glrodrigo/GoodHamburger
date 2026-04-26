using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Commands;

public class DeletePedidoCommandHandler : IRequestHandler<DeletePedidoCommand, Result<bool>>
{
    private readonly IPedidoRepository _repository;

    public DeletePedidoCommandHandler(IPedidoRepository repository) => _repository = repository;

    public async Task<Result<bool>> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
    {
        var pedidoResult = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (!pedidoResult.Success)
            return Result<bool>.Fail(pedidoResult.ErrorMessage);

        if (pedidoResult.Data is null)
            return Result<bool>.Fail($"Pedido {request.Id} não encontrado.");

        return await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}
