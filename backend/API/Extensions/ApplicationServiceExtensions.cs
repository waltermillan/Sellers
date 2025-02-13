using Core.Interfases;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200")  // Permite solo este origen en desarrollo
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader());
        });

    public static void ConfigureServices(this IServiceCollection services)
    {
        // Register the repository
        services.AddScoped<ISellerRepository, SellerRepository>();

        // Other service registrations...
        services.AddControllers();
    }
}
