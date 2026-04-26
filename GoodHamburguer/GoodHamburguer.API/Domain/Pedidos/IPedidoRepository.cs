using GoodHamburguer.Shared;

namespace GoodHamburguer.API.Domain.Pedidos;

public interface IPedidoRepository
{
    Task<Result<Pedido?>> GetByIdAsync(
        int id, 
        CancellationToken ct = default);

    Task<Result<IEnumerable<Pedido>>> GetAllAsync();

    Task<Result<int>> AddAsync(Pedido pedido);

    Task<Result<bool>> UpdateAsync(Pedido pedido);

    Task<Result<bool>> DeleteAsync(int id,
        CancellationToken ct = default);
}
