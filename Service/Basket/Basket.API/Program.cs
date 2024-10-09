using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Core.Repositary;
using Basket.Infrastructure.Repositary;
using Common.Logging;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding Serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

//api versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        // policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        policy.WithOrigins("http://localhost:3001") // Specific origin
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allow credentials
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg => { cfg.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BasketApi", Version = "v1" }); });

//SingleTon
builder.Services.AddSingleton<BasketRepositary>();

//DI

builder.Services.AddScoped<IBasketRepositary, BasketRepositary>();


//mediatR at assembly
var assembly = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetBasketByUserNameQueryHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

//Mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//register GRPC things
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg => cfg.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));


//redis connections
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration.GetConnectionString("RedisConnection"); });

//mass tranist 
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
builder.Services.AddMassTransitHostedService();




var app = builder.Build();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
