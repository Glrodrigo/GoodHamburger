using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Cardapio.Commands;

public record CreateProdutoCommand(
    string Nome, 
    string Categoria,
    string Descricao, 
    decimal Preco) : IRequest<Result<int>>;
