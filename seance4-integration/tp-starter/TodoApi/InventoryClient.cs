using System.Net.Http.Json;

namespace TodoApi;

public class InventoryClient
{
    private readonly HttpClient _client;

    public InventoryClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<InventoryItem?> GetItem(int id)
    {
        var response = await _client.GetAsync($"/inventory/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<InventoryItem>();
    }
}

public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public bool IsAvailable => Quantity > 0;
}
