
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task<int> Main(string[] args)
    {
        string url = "https://api.adviceslip.com/advice";
        using var http = new HttpClient();

        try
        {
            // Faz a requisição GET e lê a resposta como string
            string json = await http.GetStringAsync(url);

            // O JSON tem a estrutura: { "slip": { "id": 123, "advice": "..." } }
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            // Pega o campo "slip" -> "advice"
            string advice = root.GetProperty("slip").GetProperty("advice").GetString();

            Console.WriteLine();
            Console.WriteLine("Conselho de Hoje:");
            Console.WriteLine(advice);
            Console.WriteLine();

            return 0;
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine("Erro na requisição HTTP: " + httpEx.Message);
            return 1;
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine("Erro ao ler JSON: " + jsonEx.Message);
            return 2;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro inesperado: " + ex.Message);
            return 3;
        }
    }
}
