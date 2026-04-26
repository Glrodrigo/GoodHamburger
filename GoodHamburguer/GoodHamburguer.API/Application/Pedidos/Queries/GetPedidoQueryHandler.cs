using GoodHamburguer.API.Application.Mapper;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Queries;

public class GetPedidoQueryHandler : IRequestHandler<GetPedidoQuery, Result<IEnumerable<PedidoResponse>>>
{
    private readonly IPedidoRepository _repository;

    public GetPedidoQueryHandler(IPedidoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<PedidoResponse>>> Handle(GetPedidoQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync();

        if (!result.Success)
            return Result<IEnumerable<PedidoResponse>>.Fail(result.ErrorMessage);

        var response = result.Data?.Select(p => p.ToPedidoResponse());
        return Result<IEnumerable<PedidoResponse>>.Ok(response ?? []);
    }
}
