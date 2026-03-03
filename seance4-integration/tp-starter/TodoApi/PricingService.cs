using System.Text.Json;

namespace TodoApi;

public class PricingService
{
    public decimal CalculateTotal(List<decimal> prices, decimal discountPercent)
    {
        if (prices == null || prices.Count == 0)
            return 0;

        var subtotal = prices.Sum();
        var discount = subtotal * (discountPercent / 100m);
        var total = subtotal - discount;

        return Math.Round(Math.Max(total, 0), 2);
    }

    public string SerializeTodo(Todo todo)
    {
        return JsonSerializer.Serialize(todo);
    }

    public Todo DeserializeTodo(string json)
    {
        return JsonSerializer.Deserialize<Todo>(json)
            ?? throw new ArgumentException("Invalid JSON");
    }
}
