using GoodHamburguer.Shared;

namespace GoodHamburguer.API.Domain.Produtos;

public interface IProdutoRepository
{
    Task<Result<IEnumerable<Produto>>> GetAllAsync();

    Task<Result<Produto?>> GetByIdAsync(
        int id,
        CancellationToken ct = default);

    Task<Result<IEnumerable<Produto>>> GetByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken ct = default);

    Task<Result<int>> AddAsync(Produto produto);
}
