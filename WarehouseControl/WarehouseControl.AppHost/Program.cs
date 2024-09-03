var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.WarehouseControl_ApiService>("apiservice");

builder.AddProject<Projects.WarehouseControl_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();