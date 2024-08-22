using Microsoft.AspNetCore.Http.HttpResults;
using PablitosCustom.Model;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/inventoryItems/{id?}", (int? id) => {
    string jsonFilePath = "data/inventoryItems.json";
    string jsonString = File.ReadAllText(jsonFilePath);
    List<InventoryItem>? inventoryItems = JsonSerializer.Deserialize<List<InventoryItem>>(jsonString);

    if (inventoryItems == null)
    {
        return Results.NoContent();
    }

    if (id != null)
    {
        inventoryItems = inventoryItems.Where(i => i.id == id).ToList();
    }

    if (inventoryItems.Count == 0)
    {
        return Results.NotFound();
    }

    // Simulate a delay of a random time between 1 and 10 seconds
    Random random = new Random();
    int delay = random.Next(1000, 10001);
    Console.WriteLine($"Delay: {delay}");
    Thread.Sleep(delay);

    return Results.Ok(new InventoryResponse() { guid = Guid.NewGuid(), delay = delay, data = inventoryItems });
}).Produces<InventoryResponse>((int)HttpStatusCode.OK)
.Produces((int)HttpStatusCode.NotFound)
.Produces(204);

app.Run();
