using Autofac.Extensions.DependencyInjection;
using Autofac;
using MS.CoachSystem.SOAP.Contracts;
using SoapCore;
using System.ServiceModel;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRegisterCoachService, RegisterCoachService>();  // Veya uygun implementasyonu kullanýn

builder.Services.AddSoapCore();

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

app.UseRouting();  // Bu adým, routing yapýlandýrmasýný baþlatýr

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IRegisterCoachService>("/RegisterCoachService.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});


app.Run();
