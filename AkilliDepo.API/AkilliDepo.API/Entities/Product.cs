namespace AkilliDepo.API.Entities
{
    // Product sınıfımız BaseEntity'den miras alıyor. 
    // Yani CompanyId ve IsDeleted özellikleri buraya otomatik gelecek.
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string SkuCode { get; set; } = string.Empty;
        public int StockQuantity { get; set; } = 0;
    }
}