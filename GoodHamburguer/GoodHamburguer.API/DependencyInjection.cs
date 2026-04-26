using GoodHamburguer.API.Application.Cardapio.Commands;
using GoodHamburguer.API.Domain.Pedidos;
using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.API.Infrastructure.Data;
using GoodHamburguer.API.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO : Configs para caso a aplicação for utilizada em db
        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        //services.AddDbContext<AppDbContext>(options =>
        //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("db_hamburguer"));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateProdutoHandler).Assembly));

        return services;
    }
}
