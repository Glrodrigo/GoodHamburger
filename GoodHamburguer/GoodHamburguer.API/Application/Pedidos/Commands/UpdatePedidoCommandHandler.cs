using GoodHamburguer.API.Application.Mapper;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.Shared;
using MediatR;

namespace GoodHamburguer.API.Application.Pedidos.Commands;

public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, Result<PedidoResponse?>>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public UpdatePedidoCommandHandler(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<Result<PedidoResponse?>> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
    {
        var pedidoResult = await _pedidoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (!pedidoResult.Success)
            return Result<PedidoResponse?>.Fail(pedidoResult.ErrorMessage);

        if (pedidoResult.Data is null)
            return Result<PedidoResponse?>.Ok(null);

        if (request.ProdutoIds == null || request.ProdutoIds.Count == 0)
            return Result<PedidoResponse?>.Fail("O pedido deve conter ao menos um produto.");

        if (request.ProdutoIds.Count != request.ProdutoIds.Distinct().Count())
            return Result<PedidoResponse?>.Fail("Itens duplicados não são permitidos no pedido.");

        var produtosResult = await _produtoRepository.GetByIdsAsync(request.ProdutoIds, cancellationToken);

        if (!produtosResult.Success || produtosResult.Data is null)
            return Result<PedidoResponse?>.Fail($"Erro ao recuperar produtos: {produtosResult.ErrorMessage}");

        var produtos = produtosResult.Data.ToList();

        if (produtos.Count != request.ProdutoIds.Distinct().Count())
            return Result<PedidoResponse?>.Fail("Um ou mais produtos selecionados são inválidos.");

        var sanduiches = produtos.Where(p => p.Categoria == "Sanduiche").ToList();
        if (sanduiches.Count > 1)
            return Result<PedidoResponse?>.Fail("O pedido pode conter apenas um sanduíche.");

        var pedido = pedidoResult.Data;
        pedido.AtualizarItens(produtos);
        pedido.CalcularTotais(produtos);

        var updateResult = await _pedidoRepository.UpdateAsync(pedido);

        if (!updateResult.Success)
            return Result<PedidoResponse?>.Fail(updateResult.ErrorMessage);

        return Result<PedidoResponse?>.Ok(pedido.ToPedidoResponse());
    }
}
