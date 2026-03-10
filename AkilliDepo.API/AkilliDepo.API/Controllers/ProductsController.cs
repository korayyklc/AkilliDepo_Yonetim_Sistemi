using AkilliDepo.API.DTOs;
using AkilliDepo.API.Managers;
using Microsoft.AspNetCore.Mvc;

namespace AkilliDepo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _manager;
        public ProductsController(IProductManager manager) { _manager = manager; }

        [HttpGet("by-company/{companyId}")]
        public async Task<IActionResult> GetByCompany(string companyId, [FromQuery] int page = 1, [FromQuery] int pageSize = 25, [FromQuery] string? searchTerm = null)
        {
            return Ok(await _manager.GetProductsAsync(companyId, page, pageSize, searchTerm ?? ""));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto) => Ok(await _manager.CreateAsync(dto));

        // KURAL: PUT yasak, Düzenleme için POST kullanıyoruz
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto dto)
        {
            await _manager.UpdateAsync(dto);
            return Ok(new { success = true });
        }

        [HttpPost("update-stock")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockDto dto)
        {
            await _manager.UpdateStockAsync(dto);
            return Ok(new { success = true });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductDto dto)
        {
            await _manager.DeleteAsync(dto.Id, dto.CompanyId);
            return Ok(new { success = true });
        }
    }
}