using Common.Logging;
using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositary;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositary;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding Serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

//mediatR at assembly
var assembly = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetDiscountQueryHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

//Mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<ICouponRepositary, CouponRepositary>();

builder.Services.AddGrpc();



var app = builder.Build();

app.MigrationDatabase<Program>();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();

app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with grpc endpoints must be made through a grpc client");
    });
});

app.Run();
