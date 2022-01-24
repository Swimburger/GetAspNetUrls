var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

Console.WriteLine($"Urls from Program.cs before app.StartAsync(): {string.Join(", ", app.Urls)}");

await app.StartAsync();

Console.WriteLine($"Urls from Program.cs after app.StartAsync(): {string.Join(", ", app.Urls)}");

await app.WaitForShutdownAsync();