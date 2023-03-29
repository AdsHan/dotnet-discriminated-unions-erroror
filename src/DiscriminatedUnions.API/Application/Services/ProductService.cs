using DiscriminatedUnions.API.Application.DTO;
using DiscriminatedUnions.API.Common;
using DiscriminatedUnions.API.Data;
using DiscriminatedUnions.API.Data.Entities;
using ErrorOr;
using FlowControl.API.Application.OptionErrorOr;
using Microsoft.EntityFrameworkCore;

namespace DiscriminatedUnions.API.Application.Services;

public class ProductService : BaseService, IProductService
{

    private readonly CatalogDbContext _dbContext;

    public ProductService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        return await _dbContext.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProductModel> GetByIdAsync(int id)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ProductModel> GetBySkuAsync(string sku)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Sku == sku);
    }

    public async Task<ErrorOr<BaseResult>> UpdateAsync(ProductInputModel product)
    {
        ValidateDomain(product);

        if (!result.IsValid())
        {
            return result.Errors;
        }

        var productDB = await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == product.Id);

        if (productDB == null)
        {
            return Errors.Product.NotFound;
        }

        productDB.Sku = product.Sku;
        productDB.Name = product.Name;

        _dbContext.Entry(productDB).State = EntityState.Modified;
        _dbContext.Update(productDB);

        result.Response = productDB;
        return result;
    }

    public async Task<ErrorOr<BaseResult>> AddAsync(ProductInputModel product)
    {
        ValidateDomain(product);

        if (!result.IsValid())
        {
            return result.Errors;
        }

        var isIncluded = await GetBySkuAsync(product.Sku);

        if (isIncluded != null)
        {
            return Errors.Product.SkuDuplicate;
        }

        var newProduct = new ProductModel()
        {
            Sku = product.Sku,
            Name = product.Name,
        };

        _dbContext.Add(newProduct);

        await _dbContext.SaveChangesAsync();

        result.Response = newProduct.Id;
        return result;
    }

    public async Task<ErrorOr<BaseResult>> DeleteAsync(int id)
    {
        var productDB = await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);

        if (productDB == null)
        {
            return Errors.Product.NotFound;
        }

        _dbContext.Entry(productDB).State = EntityState.Deleted;
        _dbContext.Remove(productDB);

        result.Response = productDB.Id;
        return result;
    }

    private void ValidateDomain(ProductInputModel product)
    {
        if (product.Name.Length > 100)
        {
            AddError(Error.Validation(description: "O nome é muito longo"));
        }

        if (string.IsNullOrWhiteSpace(product.Name))
        {
            AddError(Error.Validation(description: "O Nome não pode ser vazio ou com espaços"));
        }

        if (string.IsNullOrWhiteSpace(product.Sku))
        {
            AddError(Error.Validation(description: "É obrigatório informar o SKU"));
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

}
