namespace AkilliDepo.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SkuCode { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
    }

    public class CreateProductDto
    {
        public string CompanyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SkuCode { get; set; } = string.Empty;
    }

    public class UpdateProductDto : CreateProductDto
    {
        public int Id { get; set; }
    }

    public class DeleteProductDto
    {
        public int Id { get; set; }
        public string CompanyId { get; set; } = string.Empty;
    }

    // Stok güncelleme için gereken paket
    public class UpdateStockDto
    {
        public int Id { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public int NewQuantity { get; set; }
    }
}