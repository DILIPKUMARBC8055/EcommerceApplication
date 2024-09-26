using Catalog.Application.Handlers;
using Catalog.Application.Mappers;
using Catalog.Core.Repositary;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositary;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CatalogAPI", Version = "v1" }); });

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
