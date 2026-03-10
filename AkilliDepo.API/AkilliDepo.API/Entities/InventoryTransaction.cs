namespace AkilliDepo.API.Entities
{
    public class InventoryTransaction : BaseEntity
    {
        public int ProductId { get; set; } // Hangi ürün?
        public int WarehouseZoneId { get; set; } // Hangi rafa/bölgeye?
        public int Quantity { get; set; } // Kaç adet?

        // İşlem türü: "IN" (Giriş) veya "OUT" (Çıkış)
        public string TransactionType { get; set; } = string.Empty;
    }
}