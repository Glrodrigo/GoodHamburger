using GoodHamburguer.API.Domain.Produtos;
using GoodHamburguer.API.Infrastructure.Data;

namespace GoodHamburguer.API.Infrastructure.Seeders;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Set<Produto>().Any())
            return;

        var produtos = new List<Produto>
        {
            new("X Burger",
                "Sanduiche",
                "Blend artesanal grelhado, queijo cheddar derretido e molho especial da casa.",
                5.00m),

            new("X Egg",
                "Sanduiche",
                "Blend suculento com ovo estrelado, queijo prato e alface crocante. Uma combinação clássica e irresistível.",
                4.50m),

            new("X Bacon",
                "Sanduiche",
                "Blend robusto com tiras de bacon crocante, cheddar duplo e molho barbecue.",
                7.00m),

            new("Batata Frita",
                "Acompanhamento",
                "Batatas palito douradas e crocantes, temperadas com sal e ervas.",
                2.00m),

            new("Refrigerante",
                "Acompanhamento",
                "Lata 350ml bem gelada — escolha entre Cola, Limão ou Laranja. Refrescante, para matar a sede.",
                2.50m),
        };

        context.Set<Produto>().AddRange(produtos);
        context.SaveChanges();
    }
}