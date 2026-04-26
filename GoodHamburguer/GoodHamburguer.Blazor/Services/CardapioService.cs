using System.Net.Http.Json;
using GoodHamburguer.Shared;

namespace GoodHamburguer.Blazor.Services;

public class CardapioService
{
    private readonly HttpClient _http;

    public CardapioService(HttpClient http) => _http = http;

    public async Task<Result<IEnumerable<ProdutoResponse>>> GetCardapioAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<Result<IEnumerable<ProdutoResponse>>>("api/cardapio")
                   ?? Result<IEnumerable<ProdutoResponse>>.Fail("Erro ao processar dados.");
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProdutoResponse>>.Fail($"Falha na comunicação: {ex.Message}");
        }
    }
}
