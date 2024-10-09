using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// Configure API Gateway to load Ocelot configuration file
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var env = context.HostingEnvironment;
    config.AddJsonFile($"ocelot.{env.EnvironmentName}.json",  true, true);
});

builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ocelot services
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

// Map controllers (if needed)
app.MapControllers();

// Set up a default endpoint (optional, but good for health checks)
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => context.Response.WriteAsync("Welcome to Ocelot API Gateway"));
});

// Run Ocelot middleware
await app.UseOcelot();
await app.RunAsync();
