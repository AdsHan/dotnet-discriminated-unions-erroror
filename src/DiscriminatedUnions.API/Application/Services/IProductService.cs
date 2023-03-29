using DiscriminatedUnions.API.Application.DTO;
using DiscriminatedUnions.API.Common;
using DiscriminatedUnions.API.Data.Entities;
using ErrorOr;

namespace DiscriminatedUnions.API.Application.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetAllAsync();
    Task<ProductModel> GetByIdAsync(int id);
    Task<ErrorOr<BaseResult>> AddAsync(ProductInputModel product);
    Task<ErrorOr<BaseResult>> UpdateAsync(ProductInputModel product);
    Task<ErrorOr<BaseResult>> DeleteAsync(int id);
}