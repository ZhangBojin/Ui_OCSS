using System.Net.Http.Headers;
using Ui_OCSS.Components.Layout;

namespace Ui_OCSS.Infrastructure.HeartbeatService;

public class HeartbeatService(IConfiguration configuration, HttpClient httpClient) : IHostedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
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
        var timer = new Timer( async _ => await SendHeartbeat(token), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
    }
    private async Task SendHeartbeat(string token)
    {
        var ip= _configuration.GetSection("Ip")["ApiGateWay"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{ip}/api/Heartbeat/OnlineHeartbeat");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            MainLayout.Number=responseContent;
        }
    }
}