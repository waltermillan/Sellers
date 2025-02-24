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

// Configurar CORS
builder.Services.ConfigureCors();

// Configurar Entity Framework y DbContext para DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnectionDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConnectionDB"))));  // Usa MariaDB

// Configurar filtros de logging para ASP.NET Core y Entity Framework Core
builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // Para toda la parte de Microsoft
builder.Logging.AddFilter("System", LogLevel.Warning); // Para logs de System

// Configuración de Serilog para escribir solo en archivo
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/todolist-.log", rollingInterval: RollingInterval.Day) // Log en archivo diario
    .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore")) // Excluir logs de Entity Framework Core
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore")) // Excluir logs de ASP.NET Core
    .CreateLogger();


builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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