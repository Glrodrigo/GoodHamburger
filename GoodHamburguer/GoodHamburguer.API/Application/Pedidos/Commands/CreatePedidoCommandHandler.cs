using MediatR;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.Shared;
using GoodHamburguer.API.Domain.Pedidos;
namespace GoodHamburguer.API.Application.Pedidos.Commands;

public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, Result<int>>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public CreatePedidoCommandHandler(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
    {
        _produtoRepository = produtoRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Result<int>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;

        if (request.ProdutoIds == null || request.ProdutoIds.Count == 0)
            return Result<int>.Fail("O pedido deve conter ao menos um produto.");

        if (request.ProdutoIds.Count != request.ProdutoIds.Distinct().Count())
            return Result<int>.Fail("Itens duplicados não são permitidos no pedido.");

        var resultProdutos = await _produtoRepository.GetByIdsAsync(request.ProdutoIds, cancellationToken);

        if (!resultProdutos.Success || resultProdutos.Data == null)
            return Result<int>.Fail($"Erro ao recuperar produtos: {resultProdutos.ErrorMessage}");

        var produtos = resultProdutos.Data.ToList();

        if (produtos.Count != request.ProdutoIds.Distinct().Count())
            return Result<int>.Fail("Um ou mais produtos selecionados são inválidos.");

        var sanduiches = produtos.Where(p => p.Categoria == "Sanduiche").ToList();
        if (sanduiches.Count > 1)
            return Result<int>.Fail("O pedido pode conter apenas um sanduíche.");

        var novoPedido = Pedido.Criar(dataPedido: now, updatePedido: now);

        foreach (var p in produtos)
            novoPedido.AdicionarItem(p.Id, p.Preco);

        novoPedido.CalcularTotais(produtos);

        return await _pedidoRepository.AddAsync(novoPedido);
    }
}
