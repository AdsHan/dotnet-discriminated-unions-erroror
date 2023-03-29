using DiscriminatedUnions.API.Application.Services;
using DiscriminatedUnions.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscriminatedUnions.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("CatalogDB"));

        services.AddScoped<IProductService, ProductService>();

        services.AddTransient<ProductPopulateService>();

        return services;
    }
}
