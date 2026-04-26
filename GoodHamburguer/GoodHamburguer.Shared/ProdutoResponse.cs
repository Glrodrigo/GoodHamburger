using System.Text.Json.Serialization;

namespace GoodHamburguer.Shared;

public class ProdutoResponse
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyOrder(2)]
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyOrder(3)]
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [JsonPropertyOrder(4)]
    [JsonPropertyName("categoria")]
    public string Categoria { get; set; } = string.Empty;

    [JsonPropertyOrder(5)]
    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }

    [JsonPropertyOrder(6)]
    [JsonPropertyName("precoFormatado")]
    public string PrecoFormatado => Preco.ToString("C");
}
