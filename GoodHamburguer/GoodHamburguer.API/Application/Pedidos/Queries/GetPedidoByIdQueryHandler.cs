using GoodHamburguer.API.Application.Mapper;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Queries;

public class GetPedidoByIdQueryHandler : IRequestHandler<GetPedidoByIdQuery, Result<PedidoResponse?>>
{
    private readonly IPedidoRepository _repository;

    public GetPedidoByIdQueryHandler(IPedidoRepository repository) => _repository = repository;

    public async Task<Result<PedidoResponse?>> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (!result.Success)
            return Result<PedidoResponse?>.Fail(result.ErrorMessage);

        if (result.Data is null)
            return Result<PedidoResponse?>.Ok(null);

        return Result<PedidoResponse?>.Ok(result.Data.ToPedidoResponse());
    }
}
