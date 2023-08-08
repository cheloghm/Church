using Church.Data;
using Church.Extensions;
using Church.Middlewares;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Church.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Church", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Add HttpClient
builder.Services.AddHttpClient();

// Add MVC services
builder.Services.AddControllers();

// Add custom services
builder.Services.AddMyServices();

var app = builder.Build();

// Order of middleware is important
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Church v1"));
app.UseRouting(); // Routing must come first
app.UseAuthentication(); // Then authentication
app.UseAuthorization(); // Then authorization
app.UseMiddleware<RoleValidationMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
