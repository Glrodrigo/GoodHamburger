using System.Text.Json.Serialization;

namespace GoodHamburguer.Shared;

public class PedidoResponse
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyOrder(2)]
    [JsonPropertyName("dataPedido")]
    public DateTime DataPedido { get; set; }

    [JsonPropertyOrder(3)]
    [JsonPropertyName("updatePedido")]
    public DateTime UpdatePedido { get; set; }

    [JsonPropertyOrder(4)]
    [JsonPropertyName("subtotal")]
    public decimal Subtotal { get; set; }

    [JsonPropertyOrder(5)]
    [JsonPropertyName("descontoPercentual")]
    public decimal DescontoPercentual { get; set; }

    [JsonPropertyOrder(6)]
    [JsonPropertyName("valorDesconto")]
    public decimal ValorDesconto { get; set; }

    [JsonPropertyOrder(7)]
    [JsonPropertyName("totalFinal")]
    public decimal TotalFinal { get; set; }

    [JsonPropertyOrder(8)]
    [JsonPropertyName("itens")]
    public IEnumerable<PedidoItemResponse> Itens { get; set; } = [];
}

public class PedidoItemResponse
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("produtoId")]
    public int ProdutoId { get; set; }

    [JsonPropertyOrder(2)]
    [JsonPropertyName("nomeProduto")]
    public string NomeProduto { get; set; } = string.Empty;

    [JsonPropertyOrder(3)]
    [JsonPropertyName("precoUnitario")]
    public decimal PrecoUnitario { get; set; }
}