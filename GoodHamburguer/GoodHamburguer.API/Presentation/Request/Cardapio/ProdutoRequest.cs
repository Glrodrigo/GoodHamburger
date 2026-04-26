using System.Text.Json.Serialization;

namespace GoodHamburguer.API.Presentation.Request.Cardapio;

public sealed class ProdutoRequest
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyOrder(2)]
    [JsonPropertyName("categoria")]
    public string Categoria { get; set; } = string.Empty;

    [JsonPropertyOrder(3)]
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [JsonPropertyOrder(4)]
    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }
}
