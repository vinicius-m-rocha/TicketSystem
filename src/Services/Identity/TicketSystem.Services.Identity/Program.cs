using Serilog;
using TicketSystem.BuildingBlocks.Logging;
using TicketSystem.BuildingBlocks.Observability;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomLogging();
builder.Services.AddCustomTracing("Identity.API");

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


Log.Information("The Identity API is starting...");

app.MapGet("/", () => "Identity.API is running!");

app.Run();