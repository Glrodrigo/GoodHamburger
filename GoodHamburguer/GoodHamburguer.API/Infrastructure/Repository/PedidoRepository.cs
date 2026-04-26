using Microsoft.EntityFrameworkCore;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.API.Infrastructure.Data;
using GoodHamburguer.Shared;

namespace GoodHamburguer.API.Infrastructure.Repository;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context) => _context = context;

    public async Task<Result<Pedido?>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var pedido = await _context.Set<Pedido>()
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            return Result<Pedido?>.Ok(pedido);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<Pedido?>.Fail($"Erro ao buscar pedido {id}: {errorMessage}");
        }
    }

    public async Task<Result<IEnumerable<Pedido>>> GetAllAsync()
    {
        try
        {
            var pedidos = await _context.Set<Pedido>()
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .AsNoTracking()
                .ToListAsync();

            return Result<IEnumerable<Pedido>>.Ok(pedidos);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<IEnumerable<Pedido>>.Fail($"Erro ao listar pedidos: {errorMessage}");
        }
    }

    public async Task<Result<int>> AddAsync(Pedido pedido)
    {
        try
        {
            await _context.Set<Pedido>().AddAsync(pedido);
            await _context.SaveChangesAsync();

            return Result<int>.Ok(pedido.Id);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<int>.Fail($"Erro ao persistir pedido: {errorMessage}");
        }
    }

    public async Task<Result<bool>> UpdateAsync(Pedido pedido)
    {
        try
        {
            var itensNoBanco = _context.Set<PedidoItem>()
                .Where(i => i.PedidoId == pedido.Id);

            _context.Set<PedidoItem>().RemoveRange(itensNoBanco);

            _context.Set<Pedido>().Update(pedido);

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<bool>.Fail($"Erro ao atualizar pedido: {errorMessage}");
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var pedido = await _context.Set<Pedido>().FindAsync([id], ct);

            if (pedido is null)
                return Result<bool>.Fail($"Pedido {id} não encontrado.");

            _context.Set<Pedido>().Remove(pedido);
            await _context.SaveChangesAsync(ct);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Result<bool>.Fail($"Erro ao remover pedido: {errorMessage}");
        }
    }
}
