using GoodHamburguer.Shared;
using System.Net.Http.Json;

namespace GoodHamburguer.Blazor.Services;

public class PedidoService
{
    private readonly HttpClient _http;

    public PedidoService(HttpClient http) => _http = http;

    public async Task<Result<IEnumerable<PedidoResponse>>> GetPedidosAsync()
    {
        try
        {
            var result = await _http.GetFromJsonAsync<Result<IEnumerable<PedidoResponse>>>("api/pedido");
            return result ?? Result<IEnumerable<PedidoResponse>>.Fail("Resposta nula da API.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PedidoService.GetPedidosAsync] {ex}");
            return Result<IEnumerable<PedidoResponse>>.Fail($"Falha na comunicação: {ex.Message}");
        }
    }

    public async Task<Result<PedidoResponse?>> GetPedidoByIdAsync(int id)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<Result<PedidoResponse?>>($"api/pedido/{id}");
            return result ?? Result<PedidoResponse?>.Fail("Resposta nula da API.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PedidoService.GetPedidoByIdAsync] {ex}");
            return Result<PedidoResponse?>.Fail($"Falha na comunicação: {ex.Message}");
        }
    }

    public async Task<Result<int>> CreatePedidoAsync(List<int> produtoIds)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/pedido", new { produtoIds });
            var result = await response.Content.ReadFromJsonAsync<Result<int>>();
            return result ?? Result<int>.Fail("Resposta nula da API.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PedidoService.CreatePedidoAsync] {ex}");
            return Result<int>.Fail($"Falha na comunicação: {ex.Message}");
        }
    }

    public async Task<Result<PedidoResponse?>> UpdatePedidoAsync(int id, List<int> produtoIds)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/pedido/{id}", new { produtoIds });
            var content = await response.Content.ReadAsStringAsync(); // lê uma vez

            Console.WriteLine($"[UpdatePedido] Status: {response.StatusCode} | Body: {content}");

            var result = System.Text.Json.JsonSerializer.Deserialize<Result<PedidoResponse?>>(
                content,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? Result<PedidoResponse?>.Fail("Resposta nula da API.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PedidoService.UpdatePedidoAsync] {ex}");
            return Result<PedidoResponse?>.Fail($"Falha na comunicação: {ex.Message}");
        }
    }

    public async Task<bool> DeletePedidoAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/pedido/{id}");
            Console.WriteLine($"[DeletePedido] Status: {response.StatusCode}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PedidoService.DeletePedidoAsync] {ex}");
            return false;
        }
    }
}
