using API.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Registra los servicios de AutoMapper
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.ConfigureServices();

// Configurar CORS
builder.Services.ConfigureCors();

// Configurar Entity Framework y DbContext para MariaDB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnectionMariaDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConnectionMariaDB"))));  // Usa MariaDB


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