using System.Text.Json.Serialization;
using Serilog;
using TeamManager.Application;
using TeamManager.Core;
using TeamManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Host.UseSerilog(((context, configuration) =>
{
    configuration.WriteTo.Console();
    configuration.WriteTo.Seq("http://localhost:5341");
}));

var app = builder.Build();

app.UseInfrastructure();
app.MapControllers();

app.Run();
