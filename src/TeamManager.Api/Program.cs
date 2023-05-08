using System.Text.Json.Serialization;
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

var app = builder.Build();

app.UseInfrastructure();
app.MapControllers();

app.Run();
