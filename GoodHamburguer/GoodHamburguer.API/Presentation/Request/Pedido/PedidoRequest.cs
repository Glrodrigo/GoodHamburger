using System.Text.Json.Serialization;

namespace GoodHamburguer.API.Presentation.Request.Pedido;

public sealed class PedidoRequest
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("produtoIds")]
    public List<int> ProdutoIds { get; set; } = [];
}
