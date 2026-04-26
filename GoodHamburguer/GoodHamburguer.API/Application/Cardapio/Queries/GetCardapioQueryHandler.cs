using GoodHamburguer.API.Application.Mapper;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Cardapio.Queries;

public class GetCardapioQueryHandler : IRequestHandler<GetCardapioQuery, Result<IEnumerable<ProdutoResponse>>>
{
    private readonly IProdutoRepository _repository;

    public GetCardapioQueryHandler(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ProdutoResponse>>> Handle(GetCardapioQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync();

        if (!result.Success)
            return Result<IEnumerable<ProdutoResponse>>.Fail(result.ErrorMessage);

        var response = result.Data?.Select(p => p.ToProdutoResponse());
        return Result<IEnumerable<ProdutoResponse>>.Ok(response ?? []);
    }
}