using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string emoji = "👽";
        Console.WriteLine("Введите токен пользователя: ");
        string token = Console.ReadLine();
        Console.WriteLine("Введите ID канала: ");
        string channelId = Console.ReadLine();
        Console.WriteLine("Введите ID сообщения: ");
        string messageId = Console.ReadLine();

        await AddReaction(token, channelId, messageId, emoji);
        Console.WriteLine("Реакция успешно добавлена!");
    }

    static async Task AddReaction(string token, string channelId, string messageId, string emoji)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);

        var url = $"https://discord.com/api/v9/channels/{channelId}/messages/{messageId}/reactions/{Uri.EscapeDataString(emoji)}/@me";
        var response = await client.PutAsync(url, null);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка при добавлении реакции: {response.StatusCode}, {error}");
        }
    }
}