namespace AkilliDepo.API.Entities
{
    public class WarehouseZone : BaseEntity
    {
        public string ZoneName { get; set; } = string.Empty; // Raf veya Bölge Adı (Örn: A-1, B-2)
        public string Description { get; set; } = string.Empty; // Açıklama
    }
}