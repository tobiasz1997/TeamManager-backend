using System.Text.Json.Serialization;
using Serilog;
using TeamManager.Application;
using TeamManager.Core;
using TeamManager.Infrastructure;
using TeamManger.Common.Extensions.Serilog;

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

builder.Host.AddSerilog();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseInfrastructure();
app.MapControllers();

app.Run();
