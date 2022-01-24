using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IServer server;

    public HomeController(IServer server)
    {
        this.server = server;
    }

    public IActionResult Index()
    {
        var addresses = server.Features.Get<IServerAddressesFeature>().Addresses;
        Console.WriteLine($"Addresses from HomeController.Index: {string.Join(", ", addresses)}");
        return View();
    }
}