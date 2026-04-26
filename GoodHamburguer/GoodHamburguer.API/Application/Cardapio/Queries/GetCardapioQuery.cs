using MediatR;
using GoodHamburguer.Shared;

namespace GoodHamburguer.API.Application.Cardapio.Queries;

public record GetCardapioQuery() 
    : IRequest<Result<IEnumerable<ProdutoResponse>>>;
