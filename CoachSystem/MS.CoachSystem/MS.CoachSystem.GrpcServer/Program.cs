using Autofac;
using Autofac.Extensions.DependencyInjection;
using MS.CoachSystem.GrpcServer.Services;
using MS.CoachSystem.Repository;
using MS.CoachSystem.Service;
using MS.CoachSystem.Service.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapProfile));

// Add services to the container.
builder.Services.AddGrpc();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ServiceModule());
    containerBuilder.RegisterModule(new RepositoryModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CoachSystemService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
