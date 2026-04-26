using GoodHamburguer.API.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutoConfig());
        modelBuilder.ApplyConfiguration(new PedidoConfig());
        modelBuilder.ApplyConfiguration(new PedidoItemConfig());

        base.OnModelCreating(modelBuilder);
    }

    public IQueryable<TEntity> FindAll<TEntity>(bool asNoTracking = true) where TEntity : class
    {
        var query = Set<TEntity>().AsQueryable();

        if (asNoTracking) 
            query = query.AsNoTracking();

        return query;
    }
}