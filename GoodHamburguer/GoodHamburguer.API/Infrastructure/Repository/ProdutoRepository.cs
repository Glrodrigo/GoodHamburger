using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.API.Infrastructure.Data;
using GoodHamburguer.Shared;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API.Infrastructure.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    public ProdutoRepository(AppDbContext context) => _context = context;

    public async Task<Result<IEnumerable<Produto>>> GetAllAsync()
    {
        try
        {
            var produtos = await _context.Set<Produto>()
                                 .AsNoTracking()
                                 .ToListAsync();

            return Result<IEnumerable<Produto>>.Ok(produtos);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<IEnumerable<Produto>>.Fail($"Erro ao obter cardápio: {errorMessage}");
        }
    }

    public async Task<Result<Produto?>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var produto = await _context.Set<Produto>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            return Result<Produto?>.Ok(produto);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<Produto?>.Fail($"Erro ao buscar produto por ID: {errorMessage}");
        }
    }

    public async Task<Result<IEnumerable<Produto>>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        try
        {
            var produtos = await _context.Set<Produto>()
                .AsNoTracking()
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(ct);

            return Result<IEnumerable<Produto>>.Ok(produtos);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<IEnumerable<Produto>>.Fail($"Erro ao buscar lista de produtos: {errorMessage}");
        }
    }

    public async Task<Result<int>> AddAsync(Produto produto)
    {
        try
        {
            await _context.Set<Produto>().AddAsync(produto);
            await _context.SaveChangesAsync();
            return Result<int>.Ok(produto.Id);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<int>.Fail($"Erro ao persistir produto: {errorMessage}");
        }
    }
}
