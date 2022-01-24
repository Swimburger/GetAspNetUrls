using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

public class MyBackgroundService : BackgroundService
{
    private readonly IServer server;
    private readonly IHostApplicationLifetime hostApplicationLifetime;

    public MyBackgroundService(IServer server, IHostApplicationLifetime hostApplicationLifetime)
    {
        this.server = server;
        this.hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"Addresses before application has started: {GetAddresses()}");

        await WaitForApplicationStarted();
        
        Console.WriteLine($"Addresses after application has started: {GetAddresses()}");
    }

    private string GetAddresses()
    {
        var addresses = server.Features.Get<IServerAddressesFeature>().Addresses;
        return string.Join(", ", addresses);
    }   
    
    private Task WaitForApplicationStarted()
    {
        var completionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        hostApplicationLifetime.ApplicationStarted.Register(() => completionSource.TrySetResult());
        return completionSource.Task;
    }
}