using Core.Interfases;
using Core.Services;
using Infrastructure.Logging;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200")  // Allows only this origin in development
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader());
        });

    public static void ConfigureServices(this IServiceCollection services)
    {
        // Register the repository
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<SaleDTOService>();

        services.AddScoped<ILoggingService, SerilogLoggingService>();

        services.AddControllers();
    }
}
