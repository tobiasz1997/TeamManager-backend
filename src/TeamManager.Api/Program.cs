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

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseInfrastructure();
app.MapControllers();

app.Run();
