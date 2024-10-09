using Common.Logging;
using EventBus.Messages.Common;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding Serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddInfraService(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "OrderingAPI", Version = "v1" }); });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Ensure the policy matches the JSON structure
});

builder.Services.AddScoped<BasketOrderingConsumer>();

//mass transit
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<BasketOrderingConsumer>();
    cfg.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumer>(ctx);

            c.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(10)));
            c.UseInMemoryOutbox();

        });
    });
});
builder.Services.AddMassTransitHostedService();
builder.Logging.AddConsole();
var app = builder.Build();


//applying migration
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsyn(context, logger).Wait();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
