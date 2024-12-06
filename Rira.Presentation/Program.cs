using Rira.Presentation.Services;
using Rira.Application;
using Rira.Persistence;
using Rira.Presentation.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
    {
        options.Interceptors.Add<GrpcExceptionInterceptor>();
        options.EnableDetailedErrors = true;
    });

builder.Services.RegisterApplicationServices()
                .RegisterPersistenceServices(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<UserServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
