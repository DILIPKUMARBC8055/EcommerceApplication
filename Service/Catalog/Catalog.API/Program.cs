using Catalog.Application.Handlers;
using Catalog.Application.Mappers;
using Catalog.Core.Repositary;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositary;
using Common.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//api versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

//Adding Serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        //policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        policy.WithOrigins("http://localhost:3001") // Specific origin
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allow credentials
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Ensure the policy matches the JSON structure
});
//register mediatR
var assembly = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetAllBrandHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
//builder.Services.AddMediatR(typeof(GetAllBrandQueryHandler).Assembly);
//register automapper
builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
//register the repositary context 
builder.Services.AddSingleton<RepositaryContext>();
//register Dependency Injections
builder.Services.AddScoped<IRepositaryContext, RepositaryContext>();
builder.Services.AddScoped<IProductRepo, ProductRepositary>();
builder.Services.AddScoped<IProductBrandsRepo, BrandRepositary>();
builder.Services.AddScoped<IProductTypesRepo, TypesRepositary>();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
