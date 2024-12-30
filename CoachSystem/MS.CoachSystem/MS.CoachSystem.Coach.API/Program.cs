using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MS.CoachSystem.Core.Configuration;
using MS.CoachSystem.Repository;
using MS.CoachSystem.Service.AutoMapper;
using MS.CoachSystem.Service;
using MS.CoachSystem.Service.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Authorization services
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));

// Configure HttpClient for AuthService
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7076/");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();
    opts.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = "www.kocluksistemiauthserver.com",
        ValidAudience = "www.kocluksistemi.koc.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FahriGedikSecurityKeyFahriGedikSecurityKeyFahriGedikSecurityKeyFahriGedikSecurityKey")),
        // Validation
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Token ömrünü ayarlar
    };
    // OnMessageReceived kaldırıldı, Authorization başlığından token alınıyor
    opts.Events = new JwtBearerEvents
    {
        // OnChallenge kaldırıldı
    };
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ServiceModule());
    containerBuilder.RegisterModule(new RepositoryModule());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
