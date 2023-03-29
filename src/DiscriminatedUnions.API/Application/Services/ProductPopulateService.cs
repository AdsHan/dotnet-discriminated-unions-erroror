using DiscriminatedUnions.API.Data;
using DiscriminatedUnions.API.Data.Entities;

namespace DiscriminatedUnions.API.Application.Services;

public class ProductPopulateService
{

    private readonly CatalogDbContext _dbContext;

    public ProductPopulateService(CatalogDbContext context)
    {
        _dbContext = context;
    }

    public async Task Initialize()
    {
        if (_dbContext.Database.EnsureCreated())
        {
            var random = new Random();

            for (int i = 1; i < 100; i++)
            {
                _dbContext.Products.Add(new ProductModel()
                {
                    Name = $"Sandalia - {i}",
                    Sku = $"A{random.Next(1, 9)}026501180022U3{random.Next(1, 9)}",
                });
            }
            _dbContext.SaveChanges();
        };
    }
}