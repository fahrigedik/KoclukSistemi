using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MS.CoachSystem.Core.Configuration;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MS.CoachSystem.Service.Services;
using MS.CoachSystem.Web.Controllers;
using Microsoft.Extensions.Options;
using MS.CoachSystem.Service.AutoMapper;
using MS.CoachSystem.Repository;
using MS.CoachSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddGrpcClient<MS.CoachSystem.Web.Protos.CoachSystem.CoachSystemClient>(options =>
{
    options.Address = new Uri("https://localhost:7023");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));

// Configure HttpClient for AuthService
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7076/");
});

builder.Services.AddSession();
// Configure JWT authentication
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
        //Validation
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Token'a ömür verildiðinde 1 saatlik ömür verildiðinde +5 dk gelir. çünkü serverlar arasý zaman aralýðýný minimize etmek için.
    };
    opts.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Cookies["AccessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/Account/Login");
            return Task.CompletedTask;
        }
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
