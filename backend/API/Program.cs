using API.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Registra los servicios de AutoMapper
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.ConfigureServices();

builder.Services.ConfigureCors();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnectionDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConnectionDB"))));  // We use MariaDB

// Configure logging filters for ASP.NET Core and Entity Framework Core
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Warning);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/todolist-.log", rollingInterval: RollingInterval.Day) 
    .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore"))
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore")) 
    .CreateLogger();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();