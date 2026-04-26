using GoodHamburguer.API.Domain.Entidades;

namespace GoodHamburguer.API.Domain.Produtos;

public sealed class Produto : Entidade
{
    public Produto() { }

    public Produto(
        string nome,
        string categoria,
        string descricao,
        decimal preco) 
    {
        Nome = nome;
        Categoria = categoria;
        Descricao = descricao;
        Preco = preco;
    }

    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; } = decimal.Zero;


    public static Produto Criar(
        string nome,
        string categoria,
        string descricao,
        decimal preco)
    {
        return new Produto(
            nome,
            categoria,
            descricao,
            preco);
    }
}
