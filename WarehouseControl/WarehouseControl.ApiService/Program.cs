using WarehouseControl.Common.Mocks;
using WarehouseControl.Common.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
    return forecast;
});

app.MapGet("/products", (string? name, string? description) =>
{
    IEnumerable<Product> result = Enumerable.Empty<Product>();
    if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(description))
        return Results.Ok<IEnumerable<Product>>(MockWarehouseData.Products);
    
    if (!String.IsNullOrEmpty(name))
    {
    
        result=result.Concat( MockWarehouseData.Products.Where(p => p.Name.ToUpperInvariant().Contains(name.ToUpperInvariant())));
        Console.WriteLine(result.ToString());
    }

    if (!String.IsNullOrEmpty(description))
    {
        result =result.Concat(MockWarehouseData.Products.Where(p => p.Description.ToUpperInvariant().Contains(description.ToUpperInvariant())));

    }


    return Results.Ok(result);

});

app.MapGet("/products/{id}", (int id) => MockWarehouseData.Products.Where(p => p.ProductId == id));



app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}