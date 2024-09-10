var builder = DistributedApplication.CreateBuilder(args);

var postgresserver = builder.AddPostgres("postgres").WithPgAdmin();;
var warehouseControldb = postgresserver.AddDatabase("postgresdb");

var apiService = builder.AddProject<Projects.WarehouseControl_ApiService>("apiservice")
                        .WithReference(warehouseControldb);

builder.AddProject<Projects.WarehouseControl_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();