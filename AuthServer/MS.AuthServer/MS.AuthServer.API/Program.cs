using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MS.AuthServer.Core.Configuration;
using MS.AuthServer.Core.Entities;
using MS.AuthServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI Register

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlCon");

    if (connectionString is null)
    {
        throw new NullReferenceException("Connection string is null");
    }

    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(typeof(DataAssembly).Assembly.FullName);
    });
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
