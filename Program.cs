using Church.Data;
using Church.Extensions;
using Church.Middlewares;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new MongoClient(connectionString);
});

// Add services to the container.
builder.Services.AddMyServices();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
