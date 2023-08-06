using Church.Data;
using Church.Extensions;
using Church.Middlewares;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new MongoClient(connectionString);
});

// Register DataContext
builder.Services.AddScoped<DataContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new DataContext(configuration);
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Church", Version = "v1" });
});

// Add HttpClient
builder.Services.AddHttpClient(); // <-- Add this line

// Add MVC services
builder.Services.AddControllers();

// Add custom services
builder.Services.AddMyServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Church v1"));
app.UseHttpsRedirection();
// Other middlewares

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.Run();
