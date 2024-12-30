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


builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));

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
        ValidAudience = "www.kocluksistemi.ogrenci.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FahriGedikSecurityKeyFahriGedikSecurityKeyFahriGedikSecurityKeyFahriGedikSecurityKey")),
        // Validation
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Token ömrünü ayarlar
    };
    opts.Events = new JwtBearerEvents
    {
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
