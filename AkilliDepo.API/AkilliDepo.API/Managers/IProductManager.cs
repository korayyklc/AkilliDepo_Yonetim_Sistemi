using AkilliDepo.API.DTOs;
using AkilliDepo.API.Entities;

namespace AkilliDepo.API.Managers
{
    public interface IProductManager
    {
        Task<PagedResult<ProductDto>> GetProductsAsync(string companyId, int page, int pageSize, string searchTerm);
        Task<Product?> GetByIdAsync(int id, string companyId);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(int id, string companyId);
        Task UpdateStockAsync(UpdateStockDto dto);
    }
}