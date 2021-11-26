using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using powman.Code;
using powman.Code.Meter;

var builder = WebApplication.CreateBuilder();

builder.WebHost.UseUrls("http://0.0.0.0:5100");

builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddSingleton<DataParser>();
builder.Services.AddSingleton<ConsumptionReporter>();
builder.Services.AddSingleton<P1Reader>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<PowerConsumptionHub>("/powerconsumption");
});

var reader = app.Services.GetService<P1Reader>();
reader.Start();

app.Run();