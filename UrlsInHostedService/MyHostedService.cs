using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

public class MyHostedService : IHostedService
{
    private readonly IServer server;
    private readonly IHostApplicationLifetime hostApplicationLifetime;

    public MyHostedService(IServer server, IHostApplicationLifetime hostApplicationLifetime)
    {
        this.server = server;
        this.hostApplicationLifetime = hostApplicationLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"Addresses before application has started: {GetAddresses()}");

        hostApplicationLifetime.ApplicationStarted.Register(
                () => Console.WriteLine($"Addresses after application has started: {GetAddresses()}"));
        
        return Task.CompletedTask;
    }

    private string GetAddresses()
    {
        var addresses = server.Features.Get<IServerAddressesFeature>().Addresses;
        return string.Join(", ", addresses);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}