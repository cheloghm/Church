using Church.Data;
using Church.Extensions;
using Church.Middlewares;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Church.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<UserMapper>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Church v1"));
app.UseRouting();
app.UseMiddleware<RoleValidationMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // <-- Make sure to include this
});


app.Run();
