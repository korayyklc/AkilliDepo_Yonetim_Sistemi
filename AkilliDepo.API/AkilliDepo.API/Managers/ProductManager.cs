using AkilliDepo.API.DTOs;
using AkilliDepo.API.Entities;
using AkilliDepo.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AkilliDepo.API.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductManager(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        // Listeleme Metodu
        public async Task<PagedResult<ProductDto>> GetProductsAsync(string companyId, int page, int pageSize, string searchTerm)
        {
            var query = _repository.GetAll(companyId).Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(x => x.Name.Contains(searchTerm) || x.SkuCode.Contains(searchTerm));

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new ProductDto { Id = x.Id, CompanyId = x.CompanyId, Name = x.Name, SkuCode = x.SkuCode, StockQuantity = x.StockQuantity })
                .ToListAsync();

            return new PagedResult<ProductDto> { Success = true, Data = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
        }

        // HATA VEREN EKSİK METOD BURASIYDI:
        public async Task<Product?> GetByIdAsync(int id, string companyId)
        {
            return await _repository.GetByIdAsync(id, companyId);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product { CompanyId = dto.CompanyId, Name = dto.Name, SkuCode = dto.SkuCode, StockQuantity = 0 };
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return new ProductDto { Id = product.Id, CompanyId = product.CompanyId, Name = product.Name, SkuCode = product.SkuCode };
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(dto.Id, dto.CompanyId);
            if (product != null) { product.Name = dto.Name; product.SkuCode = dto.SkuCode; _repository.Update(product); await _repository.SaveChangesAsync(); }
        }

        public async Task DeleteAsync(int id, string companyId)
        {
            var product = await _repository.GetByIdAsync(id, companyId);
            if (product != null) { _repository.SoftDelete(product); await _repository.SaveChangesAsync(); }
        }

        public async Task UpdateStockAsync(UpdateStockDto dto)
        {
            var product = await _repository.GetByIdAsync(dto.Id, dto.CompanyId);
            if (product != null) { product.StockQuantity = dto.NewQuantity; _repository.Update(product); await _repository.SaveChangesAsync(); }
        }
    }
}