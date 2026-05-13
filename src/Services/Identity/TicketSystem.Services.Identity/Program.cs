using Serilog;
using TicketSystem.BuildingBlocks.Logging;
using TicketSystem.BuildingBlocks.Observability;
using TicketSystem.Services.Identity.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomLogging();
builder.Services.AddCustomTracing("Identity.API");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddInfrastructureData(builder.Configuration)
    .AddOpenApi()
    .AddControllers();



var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.EnsureSeedData();
Log.Information("The Identity API is starting...");
app.MapGet("/", () => "Identity.API is running!");
app.Run();