using WarehouseControl.Common.Mocks;
using WarehouseControl.Common.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<WarehouseControlDbContext>("postgresdb");
// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};



#region Weatherforecast
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


#endregion

#region Products

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


    return Results.Ok<IEnumerable<Product>>(result);

});

app.MapGet("/products/{id}", (int id) => MockWarehouseData.Products.Where(p => p.ProductId == id));



#endregion

#region Warehouses

app.MapGet("/warehouses", (string? name, int? cityId, string? countryID, int? warehouseIdentifier) =>
{
    IEnumerable<Warehouse> results = Enumerable.Empty<Warehouse>();

    if (String.IsNullOrEmpty(name) && !cityId.HasValue && String.IsNullOrEmpty(countryID) && !warehouseIdentifier.HasValue)
    {
        return Results.Ok<IEnumerable<Warehouse>>(MockWarehouseData.Warehouses);
    }

    if (!String.IsNullOrEmpty(name))
        results=results.Concat(
            MockWarehouseData.Warehouses.Where(w => w.Name.ToUpperInvariant().Contains(name.ToUpperInvariant())));
    if (cityId > 0)
        results= results.Concat(MockWarehouseData.Warehouses.Where(w => w.CityId == cityId));
    if (!String.IsNullOrEmpty(countryID))
        results = results.Concat(MockWarehouseData.Warehouses.Where(w =>
            w.CountryId.ToUpperInvariant().Contains(countryID.ToUpperInvariant())));
    if (warehouseIdentifier > 0)
       results= results.Concat(MockWarehouseData.Warehouses.Where(w => w.WarehouseIdentifier == warehouseIdentifier));

    return Results.Ok <IEnumerable<Warehouse>>(results);
});

app.MapGet("/warehouses/{id}",(int id) =>{

    return Results.Ok<IEnumerable<Warehouse>>(MockWarehouseData.Warehouses.Where(w => w.Id == id));
}); 
#endregion

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}