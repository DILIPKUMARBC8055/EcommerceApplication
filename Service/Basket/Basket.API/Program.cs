using Basket.Application.Handlers;
using Basket.Core.Repositary;
using Basket.Infrastructure.Repositary;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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



//redis connections
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration.GetConnectionString("RedisConnection"); });
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

app.Run();
