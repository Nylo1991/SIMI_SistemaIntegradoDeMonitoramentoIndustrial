using Shared;
using System.Net.Http.Json;

var http = new HttpClient();
var random = new Random();

int index = 0;

while (true)
{
    var sensor = new SensorData
    {
        Id = index,
        Temperatura = random.Next(20, 100),
        Corrente = random.Next(10, 80),
        Timestamp = DateTime.Now
    };

    try
    {
        var response = await http.PostAsJsonAsync("https://localhost:7007/api/v1/sensores",sensor);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro: {response.StatusCode} - {erro}");
        }
        else
        {
            Console.WriteLine(
                $"Enviando Temperatura: {sensor.Temperatura}°C e Corrente: {sensor.Corrente}A");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Falha na conexão: {ex.Message}");
    }

    await Task.Delay(2000);
    index++;
}