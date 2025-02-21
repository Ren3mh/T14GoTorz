var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.GoTorz_ApiService>("apiservice");

builder.AddProject<Projects.GoTorz_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
