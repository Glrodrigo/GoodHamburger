using MediatR;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.Shared;

namespace GoodHamburguer.API.Application.Cardapio.Commands;

public class CreateProdutoHandler : IRequestHandler<CreateProdutoCommand, Result<int>>
{
    private readonly IProdutoRepository _repository;

    public CreateProdutoHandler(IProdutoRepository repository) => _repository = repository;

    public async Task<Result<int>> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Nome))
            return Result<int>.Fail("O nome do hambúrguer é obrigatório.");

        var produto = new Produto(request.Nome, request.Categoria, request.Descricao, request.Preco);
        await _repository.AddAsync(produto);

        return Result<int>.Ok(produto.Id);
    }
}
