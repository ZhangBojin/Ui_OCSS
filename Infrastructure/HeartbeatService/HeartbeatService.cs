using System.Net.Http.Headers;

namespace Ui_OCSS.Infrastructure.HeartbeatService;

public class HeartbeatService(IConfiguration configuration) : IHostedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient=new HttpClient();
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public void StartPolling(string token)
    {
        var timer = new Timer(async _ => await SendHeartbeat(token), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
    }
    private async Task SendHeartbeat(string token)
    {
        var ip= _configuration.GetSection("Ip")["ApiGateWay"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{ip}/api/Heartbeat/OnlineHeartbeat");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
    }
}